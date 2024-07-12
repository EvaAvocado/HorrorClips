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
        private float _currentPosY;
        private float _currentStartPos;

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
            _currentPosY = transform.position.y;
            transform.localScale = new Vector3(_startSize, _startSize, transform.localScale.z);
            _delta = (_endSize - _startSize) / (_distance / _offset);
            _currentStartPos = _distance - 0.1f;
        }

        private void ChangeSize(float direction)
        {
            var newPosY = transform.position.y;

            if (direction > 0 && _currentPosY + _offset < newPosY && newPosY > _currentStartPos &&
                newPosY < _distance)
            {
                transform.localScale = new Vector3(transform.localScale.y + _delta, transform.localScale.y + _delta,
                    transform.localScale.z);
                _currentPosY = newPosY;
            }
            else if (direction < 0 && _currentPosY - _offset > newPosY && newPosY > _currentStartPos &&
                     newPosY < _distance)
            {
                transform.localScale = new Vector3(transform.localScale.y - _delta, transform.localScale.y - _delta,
                    transform.localScale.z);
                _currentPosY = newPosY;
            }
        }

        public void ChangeCurrentStartPos(float value)
        {
            _currentStartPos = value;
        }
    }
}