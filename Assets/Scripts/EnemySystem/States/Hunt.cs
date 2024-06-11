using UnityEngine;

namespace EnemySystem.States
{
    public class Hunt : IEnemyState
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly Transform _transform;
        private readonly Transform _playerTransform;
        private readonly float _speed;
        
        public Hunt(SpriteRenderer spriteRenderer, Transform transform, Transform playerTransform, float speed)
        {
            _spriteRenderer = spriteRenderer;
            _transform = transform;
            _playerTransform = playerTransform;
            _speed = speed;
        }
        
        public void Enter()
        {
            
        }
        
        public void Update()
        {
            Move();
        }

        public void Exit()
        {
            
        }

        private void Move()
        {
            if (_playerTransform.position.x > _transform.position.x)
            {
                _transform.localPosition += _transform.right * (_speed * Time.deltaTime);
                _spriteRenderer.flipX = false;
            }
            else
            {
                _transform.localPosition -= _transform.right * (_speed * Time.deltaTime);
                _spriteRenderer.flipX = true;
            }
        }
    }
}