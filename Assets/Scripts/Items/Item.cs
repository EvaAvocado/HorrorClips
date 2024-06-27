using System;
using EnemySystem.Minion;
using PlayerSystem;
using UnityEngine;
using Utils;

namespace Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _speedDrop;
        [SerializeField] private ItemEnum _type;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _wallLayer;

        public static event Action OnAxeIdle;
        public static event Action OnAxeSpinLeft;
        public static event Action OnAxeSpinRight;
        
        private bool _isDropItem;

        public bool IsDropItem() => _isDropItem;

        public ItemEnum GetItemEnum() => _type;

        private void Update()
        {
            if (_isDropItem)
            {
                transform.position += transform.right * (_speedDrop * Time.deltaTime);
            }
            else if (transform.parent is not null)
            {
                transform.localPosition = Vector3.zero;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDropItem
                && _enemyLayer.Contains(other.gameObject.layer))
            {
                gameObject.SetActive(false);
                CheckEnemy(other);
            }
            
            if (_isDropItem
                && _wallLayer.Contains(other.gameObject.layer))
            {
                _isDropItem = false;
                // Realisation of the rebound
                Debug.Log("rebound");
                OnAxeIdle?.Invoke();
            }
        }

        private void CheckEnemy(Collider2D other)
        {
            if (other.TryGetComponent<Minion>(out Minion minion))
            {
                minion.Die();
            }
        }

        public Transform GetTransform() => transform;

        public void AlternativeUse(IItem item = null) 
        {
            _isDropItem = true;

            if (_spriteRenderer.flipX)
            {
                OnAxeSpinLeft?.Invoke();
            }
            else
            {
                OnAxeSpinRight?.Invoke();
            }
        }

        public void Flip(float direction)
        {
            if (direction < 0
                && _speedDrop > 0)
            {
                _spriteRenderer.flipX = true;
                _speedDrop *= -1;
            }
            else if (direction > 0
                     && _speedDrop < 0)
            {
                _spriteRenderer.flipX = false;
                _speedDrop *= -1;
            }
        }
    }
}