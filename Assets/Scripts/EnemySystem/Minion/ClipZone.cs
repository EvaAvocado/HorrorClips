using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class ClipZone : MonoBehaviour
    {
        [SerializeField] private Minion _enemy;
        [SerializeField] private LayerMask _playerLayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                _enemy.SeesPlayer();
            }
        }
    }
}