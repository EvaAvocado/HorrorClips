using System;
using Items;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        public event Action OnDie;
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _hand;
        [SerializeField] private float _speed;
        [SerializeField] private KeyCode _interactionKey;
        [SerializeField] private LayerMask _itemLayer;
        [SerializeField] private LayerMask _enemyLayer;

        private Movement _movement;
        private Interaction _interaction;

        public static event Action<float> OnMove;

        private const string HORIZONTAL = "Horizontal";
        
        private void Awake()
        {
            _movement = new Movement(_spriteRenderer, transform, _hand, _speed);
            _interaction = new Interaction(new ChangeStrategy(), _hand);
        }

        void Update()
        {
            var direction = Input.GetAxis(HORIZONTAL);
            if (direction != 0)
            {
                OnMove?.Invoke(direction);
                
                _movement.Move(direction);
                
                if (_movement.Flip(direction))
                {
                    _interaction.Flip();
                }
            }

            if (Input.GetKeyDown(_interactionKey))
            {
                _interaction.Action();

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
                _interaction.SetItemEnum(other.GetComponent<IItem>());
            }
            
            if (_enemyLayer.Contains(other.gameObject.layer))
            {
                OnDie?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer))
            {
                _interaction.SetItemEnum(null);
            }
        }
    }
}
