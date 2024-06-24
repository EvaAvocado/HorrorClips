using System;
using Level;
using Level.Clips;
using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class ClipZone : MonoBehaviour
    {
        [SerializeField] private Minion _enemy;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private BoxCollider2D _collider2D;
        [SerializeField] private Clip _currentClip;
        
        public LayerMask ClipLayer => _clipLayer;

        private void OnEnable()
        {
            if (_currentClip != null)
                SetNewCollider(_currentClip.transform.position, _currentClip.ColliderWithoutDoors.size);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer) && !_currentClip.IsEditMode)
            {
                _enemy.SeesPlayer();
                _enemy.MinionAnimation.Hunt();
            }
        }

        public void TryChangeCollider(Collider2D other)
        {
            var clip = other.GetComponent<Clip>();
            if (_currentClip != clip && clip != null)
            {
                _currentClip = clip;
                SetNewCollider(_currentClip.transform.position, _currentClip.ColliderWithoutDoors.size);
            }
        }

        private void SetNewCollider(Vector2 newPosition, Vector2 newSize)
        {
            transform.position = newPosition;
            _collider2D.size = newSize;
        }
    }
}