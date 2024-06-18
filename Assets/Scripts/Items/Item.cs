using EnemySystem.Minion;
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

        private bool _isDropItem;

        public bool IsDropItem() => _isDropItem;

        public ItemEnum GetItemEnum() => _type;

        private void Update()
        {
            if (_isDropItem)
            {
                transform.position += transform.right * (_speedDrop * Time.deltaTime);
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
            
            if (_wallLayer.Contains(other.gameObject.layer))
            {
                _isDropItem = false;
                // Realisation of the rebound
                Debug.Log("rebound");
            }
        }

        public Transform GetTransform() => transform;

        public void Drop() 
        {
            _isDropItem = true;

            if (_spriteRenderer.flipX)
            {
                _speedDrop *= -1;
            }
        }

        public void Flip() => _spriteRenderer.flipX = !_spriteRenderer.flipX;

        private void CheckEnemy(Collider2D other)
        {
            if (other.TryGetComponent<Minion>(out Minion minion))
            {
                minion.Die();
            }
        }
    }
}