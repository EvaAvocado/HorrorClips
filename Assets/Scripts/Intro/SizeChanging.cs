using System;
using PlayerSystem;
using UnityEngine;

namespace Intro
{
    public class SizeChanging : MonoBehaviour
    {
        [SerializeField] private float _startSize;
        [SerializeField] private float _endSize;
        [SerializeField] private float _distance;
        [SerializeField] private float _offset = .1f;

        private float _delta;
        private float _currentPosX;

        private void OnEnable()
        {
            Player.OnMove += ChangeSize;
        }

        private void OnDisable()
        {
            Player.OnMove -= ChangeSize;
        }

        private void Awake()
        {
            _currentPosX = transform.position.x;
            transform.localScale = new Vector3(_startSize, _startSize, transform.localScale.z);
            _delta = (_endSize-_startSize) / (_distance/_offset);
        }

        private void ChangeSize(float direction)
        {
            var newPosX = transform.position.x;
            
            if (direction > 0 && _currentPosX + _offset < newPosX && newPosX > 0 && newPosX < _distance)
            {
                transform.localScale = new Vector3(transform.localScale.x + _delta, transform.localScale.x + _delta,
                    transform.localScale.z);
                _currentPosX = newPosX;
            }
            else if (direction < 0 && _currentPosX - _offset > newPosX && newPosX > 0 && newPosX < _distance)
            {
                transform.localScale = new Vector3(transform.localScale.x - _delta, transform.localScale.x - _delta,
                    transform.localScale.z);
                _currentPosX = newPosX;
            }
        }
    }
}