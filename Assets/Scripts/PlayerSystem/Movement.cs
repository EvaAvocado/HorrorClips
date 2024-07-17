using UnityEngine;

namespace PlayerSystem
{
    public class Movement
    {
        private readonly Rigidbody2D _rb;
        private readonly SpriteRenderer[] _playerSprites;
        private readonly Transform _playerTransform;
        private readonly Transform _handTransform;
        private float _speed;

        public float Speed
        {
            set => _speed = value;
        }

        public Movement(Rigidbody2D rb, SpriteRenderer[] playerSprites, Transform playerTransform, Transform handTransform, float speed)
        {
            _rb = rb;
            _playerSprites = playerSprites;
            _playerTransform = playerTransform;
            _handTransform = handTransform;
            _speed = speed;
        }
        
        public void Move(float direction, bool horizontal)
        {
            if (horizontal)
            {
                // _playerTransform.position += _playerTransform.right * (direction * _speed * Time.deltaTime);
                _rb.velocity = _playerTransform.right * (direction * _speed);
            }
            else
            {
                _playerTransform.position += _playerTransform.up * (direction * _speed * Time.deltaTime);
            }
            
        }

        public bool Flip(float direction)
        {
            if (direction > 0
                && _playerSprites[0].flipX)
            {
                _playerSprites[0].flipX = false;
                _playerSprites[1].flipX = false;
                _handTransform.localPosition *= -1;
                
                return true;
            }
            
            if (direction < 0
                && !_playerSprites[0].flipX)
            {
                _playerSprites[0].flipX = true;
                _playerSprites[1].flipX = true;
                _handTransform.localPosition *= -1;

                return true;
            }

            return false;
        }
    }
}