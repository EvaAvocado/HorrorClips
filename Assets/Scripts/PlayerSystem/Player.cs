using System;
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
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _hand;
        [SerializeField] private Transform _head;
        [SerializeField] private float _speed;
        [SerializeField] private KeyCode _interactionKey;
        [SerializeField] private LayerMask _itemLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _clipLayer;

        private Movement _movement;
        private Interaction _interaction;
        private bool _isEditMode;

        public static event Action<float> OnMove;
        public static event Action OnIdle;
        public static event Action OnFlip;

        private const string HORIZONTAL = "Horizontal";

        public bool HaveFlashlight => _interaction.HaveFlashlight;

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += SetIsCanMove;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= SetIsCanMove;
        }

        private void SetIsCanMove(bool status)
        {
            _isEditMode = status;
            if (_isEditMode) OnIdle?.Invoke();
        }

        private void Awake()
        {
            _movement = new Movement(_spriteRenderer, transform, _hand, _speed);
            _interaction = new Interaction(new ChangeStrategy(), _hand, _head);
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
                    _interaction.Flip();
                    OnFlip?.Invoke();
                }
            }
            else if (!_isEditMode)
            {
                OnIdle?.Invoke();
            }

            if (Input.GetKeyDown(_interactionKey))
            {
                if (!_interaction.Action())
                {
                    _interaction.SetItem(null);
                }

                if (_spriteRenderer.flipX)
                {
                    _interaction.Flip();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer))
            {
                _interaction.SetItem(other.GetComponent<IItem>());
            }
            
            if (_enemyLayer.Contains(other.gameObject.layer))
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
            if (_itemLayer.Contains(other.gameObject.layer))
            {
                _interaction.SetItem(null);
            }
            
            if (_clipLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                other.GetComponent<Clip>().PlayerExit();
            }
        }
    }
}
