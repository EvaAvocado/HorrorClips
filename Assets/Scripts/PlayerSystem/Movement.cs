using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly Transform _transform;
        private readonly float _speed;
        
        public Movement(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }
        
        public void Move(float direction)
        {
            _transform.position += _transform.right * (direction * _speed * Time.deltaTime);
        }
    }
}