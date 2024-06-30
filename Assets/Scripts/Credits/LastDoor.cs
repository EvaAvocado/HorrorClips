using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Credits
{
    public class LastDoor : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private UnityEvent _enterDoor;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                _enterDoor?.Invoke();
            }
        }
    }
}
