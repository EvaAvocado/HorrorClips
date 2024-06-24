using System;
using EnemySystem.States;
using PlayerSystem;
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
        [SerializeField] private ClipZone _clipZone;
        [SerializeField] private GameObject _parent;
        [SerializeField] private MinionAnimation _minionAnimation;

        private IStateMachine _stateMachine;

        public GameObject Parent => _parent;
        public MinionAnimation MinionAnimation => _minionAnimation;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private void Awake()
        {
            _stateMachine = new EnemyStateMachine();
            _playerTransform = FindObjectOfType<Player>().transform;
            _stateMachine.CreateStates(_spriteRenderer, transform, _playerTransform, _speed);
            _stateMachine.ChangeState<Wait>();
            _camera = Camera.main;
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
                _minionAnimation.Lost();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_clipZone.ClipLayer.Contains(other.gameObject.layer))
            {
                _clipZone.TryChangeCollider(other);
            }
        }

        public void SeesPlayer() => _stateMachine.ChangeState<Hunt>();

        public void Wait() => _stateMachine.ChangeState<Wait>();

        public void LostPlayer() => _stateMachine.ChangeState<Wait>();
        
        public void Die() => _stateMachine.ChangeState<Die>();
    }
}