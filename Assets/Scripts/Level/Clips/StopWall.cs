using System;
using UnityEngine;
using Utils;

namespace Level
{
    public class StopWall : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private bool _isDeadEnd;

        public bool IsDeadEnd => _isDeadEnd;

        public LayerMask ClipLayer => _clipLayer;

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (_clipLayer.Contains(other.gameObject.layer) && !_isDeadEnd)
        //     {
        //         _collider.isTrigger = true;
        //     }
        // }
        //
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (_clipLayer.Contains(other.gameObject.layer) && !_isDeadEnd)
        //     {
        //         _collider.isTrigger = false;
        //     }
        // }

        public void EnableWall(bool isEnable){
            _collider.isTrigger = isEnable;
            //Debug.Log(name);
        }

        public bool GetEnableWall => _collider.isTrigger;
    }
}