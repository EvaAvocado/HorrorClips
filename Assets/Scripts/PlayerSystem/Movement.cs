using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly SpriteRenderer _playerSprite;
        private readonly Transform _playerTransform;
        private readonly Transform _handTransform;
        private readonly float _speed;
        
        public Movement(SpriteRenderer playerSprite, Transform playerTransform, Transform handTransform, float speed)
        {
            _playerSprite = playerSprite;
            _playerTransform = playerTransform;
            _handTransform = handTransform;
            _speed = speed;
        }
        
        public void Move(float direction)
        {
            _playerTransform.position += _playerTransform.right * (direction * _speed * Time.deltaTime);
        }

        public bool Flip(float direction)
        {
            if (direction > 0
                && _playerSprite.flipX)
            {
                _playerSprite.flipX = false;
                _handTransform.localPosition *= -1;
                
                return true;
            }
            
            if (direction < 0
                && !_playerSprite.flipX)
            {
                _playerSprite.flipX = true;
                _handTransform.localPosition *= -1;

                return true;
            }

            return false;
        }
    }
}