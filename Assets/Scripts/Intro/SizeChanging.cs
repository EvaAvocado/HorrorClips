using System;
using UnityEngine;

namespace Intro
{
    public class SizeChanging : MonoBehaviour
    {
        [SerializeField] private float _startSize;
        [SerializeField] private float _endSize;
        [SerializeField] private float _distance;

        private float _delta;

        private void Awake()
        {
            transform.localScale = new Vector3(_startSize, _startSize, transform.localScale.z);
            _delta = (_endSize - _startSize) / _distance;
        }

        private void Update()
        {
            if(transform)
        }
    }
}
