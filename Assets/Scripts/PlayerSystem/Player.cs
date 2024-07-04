using System;
using System.Collections.Generic;
using Items;
using Items.Strategy;
using Level;
using Level.Clips;
using UI;
using UnityEngine;
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
        [SerializeField] private KeyCode _interactionKey;
        [SerializeField] private LayerMask _itemLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private CapsuleCollider2D _playerCollider;
        [SerializeField] private GameObject _hint;
        [SerializeField] private FlashlightOnPlayer _flashlight;
        [SerializeField] private bool _isCantStop;
        
        private Movement _movement;
        private Interaction _interaction;
        private bool _isEditMode;
        private bool _isHoldAxe;
        private bool _isTriggerForItem;
        private float _pressingTime;

        public static event Action<float> OnMove;
        public static event Action OnIdle;
        public static event Action OnFlip;
        public static event Action OnHold;
        public static event Action OnThrow;
        public static event Action OnRelease;
        public static event Action OnSwing;

        private const string HORIZONTAL = "Horizontal";
        public Movement Movement => _movement;
        public SpriteRenderer[] SpriteRenderers => _spriteRenderers;
        public bool HaveFlashlight => _interaction.HaveFlashlight;

        public bool IsCantStop
        {
            set => _isCantStop = value;
        }

        private void Awake()
        {
            _movement = new Movement(_spriteRenderers, transform, _hand, _speed);
            _interaction = new Interaction(new ChangeStrategy(_animators), _hand, this);
        }
        
        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
        }

        private void Update()
        {
            var direction = Input.GetAxis(HORIZONTAL);
            if (direction != 0 && !_isEditMode)
            {
                InvokeOnMove(direction);
                
                _movement.Move(direction);
                
                if (_movement.Flip(direction))
                {
                    _interaction.Flip(direction);
                    OnFlip?.Invoke();
                }
                
                _pressingTime = 0;
                _isHoldAxe = false;
            }
            else if (!_isEditMode && !_isCantStop)
            {
                OnIdle?.Invoke();
            }

            if (!_isTriggerForItem 
                && Input.GetKey(_interactionKey)
                && _interaction.HaveAxeInHand)
            {
                _pressingTime += Time.deltaTime;

                if (!_isHoldAxe)
                {
                    OnThrow?.Invoke();
                }
            }
            
            if (Input.GetKeyUp(_interactionKey))
            {
                if (_interaction.HaveAxeInHand
                    && _isTriggerForItem)
                {
                    OnSwing?.Invoke();
                    return;
                }
                
                if (!_interaction.Action(_isHoldAxe))
                {
                    _interaction.SetItem(null);
                }

                if (_spriteRenderers[0].flipX)
                {
                    _interaction.Flip(-1);
                }
                
                if (_isHoldAxe)
                {
                    OnRelease?.Invoke();
                }
                
                _pressingTime = 0;
                _isHoldAxe = false;
            }
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
            
            if (_isEditMode)
            {
                OnIdle?.Invoke();
                _playerCollider.enabled = false;
            }
            else
            {
                _playerCollider.enabled = true;
            }
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
            }
            
            if (_enemyLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                OnDie?.Invoke();
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
                || iitem.GetItemEnum() == ItemEnum.AXE))
            {
                _interaction.SetItem(other.GetComponent<IItem>());
                _isTriggerForItem = true;
                _hint.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                _interaction.SetItem(null);
                _isTriggerForItem = false;
                _hint.SetActive(false);
            }
            
            if (_clipLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                other.GetComponent<Clip>().PlayerExit();
            }
        }

        private bool CheckItem(IItem item)
        {
            if (item.GetItemEnum() == ItemEnum.AXE
                && _interaction.HaveAxeInHand)
            {
                return false;
            }
            
            return true;
        }

        public void UseAxe()
        {
            if (!_interaction.Action(_isHoldAxe))
            {
                _interaction.SetItem(null);
                _isTriggerForItem = false;
            }
            
            _pressingTime = 0;
            _isHoldAxe = false;
        }

        public void InvokeOnMove(float direction)
        {
            OnMove?.Invoke(direction);
        }

        public void OnFlashlight()
        {
            _flashlight.OnFlashlight();
        }
        
        public void HoldAxe() => _isHoldAxe = true;
        public void NotHoldAxe() => _isHoldAxe = false;
        public void DropAxe() => _interaction.Drop();
        public void CheckAxe() => _interaction.CheckAxe();
    }
}
