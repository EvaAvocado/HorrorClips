using System;
using System.Collections.Generic;
using Items;
using Items.Strategy;
using Level;
using Level.Clips;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        public event Action OnDie;
        
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private Transform _hand;
        [SerializeField] private Transform _head;
        [SerializeField] private List<Animator> _animators;
        [SerializeField] private float _speed;
        [SerializeField] private KeyCode _interactionKey;
        [SerializeField] private LayerMask _itemLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private CapsuleCollider2D _playerCollider;

        private Movement _movement;
        private Interaction _interaction;
        private bool _isEditMode;
        private bool _isHoldAxe;
        private float _pressingTime;

        public static event Action<float> OnMove;
        public static event Action OnIdle;
        public static event Action OnFlip;
        public static event Action OnHold;
        public static event Action OnThrow;
        public static event Action OnRelease;

        private const string HORIZONTAL = "Horizontal";

        public SpriteRenderer[] SpriteRenderers => _spriteRenderers;
        public bool HaveFlashlight => _interaction.HaveFlashlight;

        private void Awake()
        {
            _movement = new Movement(_spriteRenderers, transform, _hand, _speed);
            _interaction = new Interaction(new ChangeStrategy(_animators), _hand);
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
                OnMove?.Invoke(direction);
                
                _movement.Move(direction);
                
                if (_movement.Flip(direction))
                {
                    _interaction.Flip(direction);
                    OnFlip?.Invoke();
                }
            }
            else if (!_isEditMode)
            {
                OnIdle?.Invoke();
            }

            if (Input.GetKey(_interactionKey)
                && _interaction.HaveAxeInHand)
            {
                _pressingTime += Time.deltaTime;

                if (_pressingTime >= 4)
                {
                    // OnHold?.Invoke();
                }
                else
                {
                    OnThrow?.Invoke();
                }
            }
            
            if (Input.GetKeyUp(_interactionKey))
            {
                if (!_interaction.Action(_pressingTime, _isHoldAxe))
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
            if (_itemLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                _interaction.SetItem(other.GetComponent<IItem>());
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

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                _interaction.SetItem(null);
            }
            
            if (_clipLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                other.GetComponent<Clip>().PlayerExit();
            }
        }
        
        public void HoldAxe() => _isHoldAxe = true;
        public void DropAxe() => _interaction.Drop();
    }
}
