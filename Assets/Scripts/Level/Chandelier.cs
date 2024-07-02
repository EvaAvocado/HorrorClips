using System;
using EnemySystem.Minion;
using UnityEngine;
using Utils;

namespace Level
{
    public class Chandelier : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private LayerMask _enemyLayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_enemyLayer.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out ClipZoneFinder minion))
                {
                    minion.Minion.Die();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_enemyLayer.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out ClipZoneFinder minion))
                {
                    minion.Minion.Die();
                }
            }
        }

        public void EnableCollider(bool isEnable)
        {
            _collider2D.enabled = isEnable;
        }
    }
}