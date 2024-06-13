using EnemySystem.States;
using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class Minion : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _axeLayer;

        private IStateMachine _stateMachine;
        
        private void Awake()
        {
            _stateMachine = new EnemyStateMachine();
            _stateMachine.CreateStates(_spriteRenderer, transform, _playerTransform, _speed);
            _stateMachine.ChangeState<Wait>();
        }

        private void Update()
        {
            _stateMachine.UpdateState();

            CheckCamera();
        }

        private void CheckCamera()
        {
            var viewPosition = _camera.WorldToViewportPoint(transform.position);
            if (_stateMachine.GetState() is Hunt
                && (viewPosition.x < -0.03f
                || viewPosition.x > 1.03f))
            {
                LostPlayer();
            }
        }

        public void SeesPlayer() => _stateMachine.ChangeState<Hunt>();

        public void Wait() => _stateMachine.ChangeState<Wait>();

        public void LostPlayer() => _stateMachine.ChangeState<Wait>();
        
        public void Die() => _stateMachine.ChangeState<Die>();
    }
}