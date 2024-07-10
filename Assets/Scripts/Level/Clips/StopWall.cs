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

        private bool _canHaveCollider;

        public bool IsDeadEnd => _isDeadEnd;
        public bool CanHaveCollider => _canHaveCollider;
        public LayerMask ClipLayer => _clipLayer;
//TODO
        public void EnableWall(bool isEnable)
        {
            _collider.isTrigger = isEnable;
        }

        public void ClipExit()
        {
            _canHaveCollider = false;
        }

        public bool GetEnableWall => _collider.isTrigger;
    }
}