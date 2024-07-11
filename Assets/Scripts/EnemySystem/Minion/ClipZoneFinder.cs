using System;
using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class ClipZoneFinder : MonoBehaviour
    {
        [SerializeField] private ClipZone _clipZone;
        [SerializeField] private Minion _minion;
        [SerializeField] private LayerMask _darkMask;

        public Minion Minion => _minion;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_clipZone.ClipLayer.Contains(other.gameObject.layer))
            {
                _clipZone.TryChangeCollider(other);
            }
            
            if (_darkMask.Contains(other.gameObject.layer))
            {
                _minion.InDark = true;
            } 
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (_darkMask.Contains(other.gameObject.layer))
            {
                _minion.InDark = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_darkMask.Contains(other.gameObject.layer))
            {
                _minion.InDark = false;
            }
        }
    }
}