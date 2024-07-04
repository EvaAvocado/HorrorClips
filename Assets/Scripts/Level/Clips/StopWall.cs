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

        public void EnableWall(bool isEnable)
        {
            _collider.isTrigger = isEnable;
        }

        public bool GetEnableWall => _collider.isTrigger;
    }
}