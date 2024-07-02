using Level;
using PlayerSystem;
using UnityEngine;

namespace EnemySystem.CreatureSystem
{
    public class Creature : MonoBehaviour, ITransparent
    {
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _playerDirection;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private void OnEnable()
        {
            Player.OnMove += PlayerMove;
            Player.OnIdle += PlayerIdle;
        }

        private void OnDisable()
        {
            Player.OnMove -= PlayerMove;
            Player.OnIdle -= PlayerIdle;
        }

        private void Update()
        {
            if (_playerDirection == 0)
            {
                _animator.speed = 0.5f;
                transform.position += transform.right * (_baseSpeed * Time.deltaTime);
            }
            else
            {
                _animator.speed = 1f;
                transform.position += transform.right * (_runSpeed * Time.deltaTime);
            }
        }

        private void PlayerMove(float direction) => _playerDirection = direction;
        private void PlayerIdle() => _playerDirection = 0;
    }
}
