using System;
using UnityEngine;
using Utils;

namespace EnemySystem.CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        public event Action OnPlayerKill;
        
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private LayerMask _playerLayer;
        
        private const string HORIZONTAL = "Horizontal";
        
        private void Update()
        {
            if (Input.GetAxis(HORIZONTAL) == 0)
            {
                transform.position += transform.right * (_baseSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += transform.right * (_runSpeed * Time.deltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                OnPlayerKill?.Invoke();
            }
        }
    }
}
