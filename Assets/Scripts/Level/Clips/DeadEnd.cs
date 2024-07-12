using System;
using UnityEngine;
using Utils;

namespace Level.Clips
{
    public class DeadEnd : MonoBehaviour
    {
        [SerializeField] private StopWall _stopWall;
        [SerializeField] private BoxCollider2D _collider;

        private void OnEnable()
        {
            if (_stopWall.IsDeadEnd)
            {
                _collider.isTrigger = false;
            }

            if (!_stopWall.CanHaveCollider)
            {
                _collider.isTrigger = true;
            }
        }
        
         private void OnTriggerEnter2D(Collider2D other)
         {
             if (_stopWall.ClipLayer.Contains(other.gameObject.layer) && !_stopWall.IsDeadEnd
                 && _stopWall.CanHaveCollider)
             {
                 gameObject.layer = default;
                 _collider.isTrigger = true;
             }
         }

         private void OnTriggerStay2D(Collider2D other)
         {
             if (_stopWall.ClipLayer.Contains(other.gameObject.layer) && !_stopWall.IsDeadEnd
                 && _stopWall.CanHaveCollider)
             {
                 gameObject.layer = default;
                 _collider.isTrigger = true;
             }
         }

         private void OnTriggerExit2D(Collider2D other)
         {
             if (_stopWall.ClipLayer.Contains(other.gameObject.layer) && !_stopWall.IsDeadEnd
                 && _stopWall.CanHaveCollider)
             {
                 gameObject.layer = 9;
                 _collider.isTrigger = false;
             }
        }
    }
}
