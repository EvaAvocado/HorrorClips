using System;
using UnityEngine;
using Utils;

namespace Level
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _clipLayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _collider.isTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _collider.isTrigger = false;
            }
        }
    }
}