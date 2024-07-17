using System;
using System.Collections.Generic;
using EnemySystem.CreatureSystem;
using EnemySystem.Minion;
using Items;
using Items.Strategy;
using Level;
using Level.Clips;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utils;

namespace PlayerSystem
{
    public class Player : MonoBehaviour, ITransparent
    {
        public event Action OnDie;
        
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private Transform _hand;
        [SerializeField] private List<Animator> _animators;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _itemLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private CapsuleCollider2D _playerCollider;
        [SerializeField] private GameObject _hint;
        [SerializeField] private FlashlightOnPlayer _flashlight;
        [SerializeField] private bool _isCantStop;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Press _pressButtons;
        [SerializeField] private UnityEvent _dieEvent;

        [SerializeField] private bool _isIntro;
        
        private Movement _movement;
        private Interaction _interaction;
        private bool _isEditMode;
        private bool _isHoldAxe;
        private bool _isTriggerForItem;
        private bool _isFlashlight;
        private bool _isOpenMenu;
        private bool _isTV;

        public static event Action<float> OnMove;
        public static event Action OnIdle;
        public static event Action OnFlip;
        public static event Action OnHold;
        public static event Action OnThrow;
        public static event Action OnRelease;
        public static event Action OnSwing;
        public static event Action OnHasFlashlight;

        private const string HORIZONTAL = "Horizontal";
        public Movement Movement => _movement;
        public SpriteRenderer[] SpriteRenderers => _spriteRenderers;
        public bool HaveFlashlight => _interaction.HaveFlashlight;

        public bool IsEditMode => _isEditMode;

        public AudioSource AudioSource => _audioSource;

        public bool IsIntro => _isIntro;

        public bool IsInTheDark { get; set; }

        public bool IsCantStop
        {
            set => _isCantStop = value;
        }

        public Movement MovementClass => _movement;

        private void Awake()
        {
            _movement = new Movement(_spriteRenderers, transform, _hand, _speed);
            _interaction = new Interaction(new ChangeStrategy(_animators), _hand, this, _pressButtons);
        }
        
        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
            MenuManager.OnMenuOpen += MenuOpen;
            MenuManager.OnMenuClose += MenuClose;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
            MenuManager.OnMenuOpen -= MenuOpen;
            MenuManager.OnMenuClose -= MenuClose;
        }

        private void Update()
        {
            var direction = Input.GetAxis(HORIZONTAL);
            if (direction != 0 && !_isEditMode)
            {
                InvokeOnMove(direction);

                if (!_isIntro)
                {
                    _movement.Move(direction, true);
                }
                else
                {
                    _movement.Move(direction, false);
                }
                
                if (_movement.Flip(direction))
                {
                    _interaction.Flip(direction);
                    OnFlip?.Invoke();
                }
                
                _isHoldAxe = false;
            }
            else if (!_isEditMode && !_isCantStop)
            {
                OnIdle?.Invoke();
            }

            if (!_isTriggerForItem 
                && Input.GetKeyUp(KeyCode.Q)
                && _interaction.HaveAxeInHand
                && !_isOpenMenu
                && !_isEditMode)
            {
                OnThrow?.Invoke();
            }
            
            if ((Input.GetKeyUp(KeyCode.E)
                || Input.GetKeyUp(KeyCode.Q))
                && !_isOpenMenu
                && !_isEditMode)
            {
                if (_interaction.HaveAxeInHand
                    && _isTriggerForItem
                    && !_isTV
                    && Input.GetKeyUp(KeyCode.E))
                {
                    OnSwing?.Invoke();
                    return;
                }
                
                if (Input.GetKeyUp(KeyCode.E) 
                    && !_interaction.Action(_isHoldAxe))
                {
                    _movement.Move(0.0001f, true);
                    _interaction.SetItem(null);
                    _hint.SetActive(false);
                    _pressButtons.SetCantPress(PressButtonEnum.E);
                    _isTriggerForItem = false;
                }

                if (_spriteRenderers[0].flipX)
                {
                    _interaction.Flip(-1);
                }
            }
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
            
            if (_isEditMode)
            {
                OnIdle?.Invoke();
                _playerCollider.isTrigger = true;
            }
            else
            {
                _playerCollider.isTrigger = false;
            }

            CheckButton();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer) && !_isEditMode
                && other.TryGetComponent<IItem>(out IItem iitem)
                && iitem.CheckUse(_interaction.HaveAxeInHand)
                && CheckItem(iitem)
                && (_isTriggerForItem == false
                || iitem.GetItemEnum() == ItemEnum.AXE))
            {
                IItem item = other.GetComponent<IItem>();
                _interaction.SetItem(item);
                _isTriggerForItem = true;
                _hint.SetActive(true);
                _pressButtons.SetCanPress(PressButtonEnum.E);
                _pressButtons.SetCantPress(PressButtonEnum.Q);

                if (item.GetItemEnum() == ItemEnum.TV)
                {
                    _isTV = true;
                }
            }
            
            if (_enemyLayer.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out Creature creature))
                {
                    Die();
                }
                else if (!_isEditMode)
                {
                    Die();
                }
            }
            
            if (_clipLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                other.GetComponent<Clip>().PlayerEnter();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer) && !_isEditMode
                && other.TryGetComponent<IItem>(out IItem iitem)
                && iitem.CheckUse(_interaction.HaveAxeInHand)
                && CheckItem(iitem)
                && (_isTriggerForItem == false 
                || iitem.GetItemEnum() == ItemEnum.AXE)
                && !_isOpenMenu)
            {
                var item = other.GetComponent<IItem>();
                _interaction.SetItem(item);
                _isTriggerForItem = true;
                _hint.SetActive(true);
                _pressButtons.SetCanPress(PressButtonEnum.E);
                _pressButtons.SetCantPress(PressButtonEnum.Q);
                
                if (item.GetItemEnum() == ItemEnum.TV)
                {
                    _isTV = true;
                }
            }
            
            if (_enemyLayer.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out Creature creature))
                {
                    Die();
                }
                else if (!_isEditMode)
                {
                    Die();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                _interaction.SetItem(null);
                _isTriggerForItem = false;
                _hint.SetActive(false);
                _isTV = false;
                _pressButtons.SetCantPress(PressButtonEnum.E);

                if (_interaction.HaveAxeInHand)
                {
                    _pressButtons.SetCanPress(PressButtonEnum.Q);
                }
            }
            
            if (_clipLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                other.GetComponent<Clip>().PlayerExit();
            }
        }

        private void Die()
        {
            _dieEvent?.Invoke();
            OnDie?.Invoke();
        }

        private bool CheckItem(IItem item)
        {
            if (item.GetItemEnum() == ItemEnum.FLASHLIGHT
                && _isFlashlight)
            {
                return false;
            }
            
            if (item.GetItemEnum() == ItemEnum.AXE
                && _interaction.HaveAxeInHand)
            {
                return false;
            }
            
            return true;
        }

        private void MenuOpen()
        {
            _isOpenMenu = true;
            CheckButton();
        }

        private void MenuClose()
        {
            _isOpenMenu = false;
            CheckButton();
        }

        private void CheckButton()
        {
            if (_isOpenMenu || _isEditMode)
            {
                _pressButtons.SetCantPress(PressButtonEnum.E);
                _pressButtons.SetCantPress(PressButtonEnum.Q);
            }
            else
            {
                if (_interaction.HaveAxeInHand)
                {
                    _pressButtons.SetCanPress(PressButtonEnum.Q);
                }

                if (_isTriggerForItem)
                {
                    _pressButtons.SetCanPress(PressButtonEnum.E);
                }
            }
        }

        public void UseAxe()
        {
            if (!_interaction.Action(_isHoldAxe))
            {
                _interaction.SetItem(null);
                _isTriggerForItem = false;
            }
            
            _isHoldAxe = false;
        }

        public void InvokeOnMove(float direction)
        {
            OnMove?.Invoke(direction);
        }

        public void OnFlashlight()
        {
            _isFlashlight = true;
            OnHasFlashlight?.Invoke();
            _flashlight.OnFlashlight();
        }

        public void SetIsIntroTrue()
        {
            _isIntro = true;
        }

        public void PressE()
        {
            if (_isOpenMenu
                && _isEditMode)
            {
                return;
            }
            
            if (_interaction.HaveAxeInHand
                && _isTriggerForItem)
            {
                OnSwing?.Invoke();
                return;
            }
                
            if (!_interaction.Action(_isHoldAxe))
            {
                _movement.Move(0.0001f, true);
                _interaction.SetItem(null);
                _hint.SetActive(false);
                _pressButtons.SetCantPress(PressButtonEnum.E);
                _isTriggerForItem = false;
            }

            if (_spriteRenderers[0].flipX)
            {
                _interaction.Flip(-1);
            }
        }

        public void PressQ()
        {
            if (!_isTriggerForItem 
                && _interaction.HaveAxeInHand
                && !_isOpenMenu
                && !_isEditMode)
            {
                OnThrow?.Invoke();
            }
        }
        
        public void HoldAxe() => _isHoldAxe = true;
        public void NotHoldAxe() => _isHoldAxe = false;
        public void DropAxe() => _interaction.Drop();
        public void CheckAxe() => _interaction.CheckAxe(_hint);
    }
}
