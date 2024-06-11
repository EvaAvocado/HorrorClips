using System;
using Items;
using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _hand;
        [SerializeField] private float _speed;
        [SerializeField] private KeyCode _interactionKey;
        [SerializeField] private LayerMask _itemLayer;

        private Movement _movement;
        private Interaction _interaction;

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
                _movement.Move(direction);
                _movement.Flip(direction);
            }

            if (Input.GetKeyDown(_interactionKey))
            {
                _interaction.Action();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_itemLayer.Contains(other.gameObject.layer))
            {
                _interaction.SetItemEnum(other.transform);
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
