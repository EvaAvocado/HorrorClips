using System;
using EnemySystem.States;
using Level;
using PlayerSystem;
using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class Minion : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _axeLayer;
        [SerializeField] private GameObject _parent;
        [SerializeField] private MinionAnimation _minionAnimation;
        [SerializeField] private Vector2 _distanceToLostPlayer;

        private IStateMachine _stateMachine;
        private Player _player;
        private EditManager _editManager;
        
        public static event Action OnDieMinion;

        public GameObject Parent => _parent;
        public MinionAnimation MinionAnimation => _minionAnimation;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        private void Awake()
        {
            _stateMachine = new EnemyStateMachine();
            _playerTransform = FindObjectOfType<Player>().transform;
            _stateMachine.CreateStates(_spriteRenderer, transform, _playerTransform, _speed);
            _stateMachine.ChangeState<Wait>();
            _editManager = FindObjectOfType<EditManager>();
        }

        private void Update()
        {
            _stateMachine.UpdateState();

            if (_editManager.IsEditMode && _stateMachine.GetState() is Hunt)
            {
                LostPlayer();
                _minionAnimation.Lost();
            }

            CheckPlayerPos();
        }

        private void CheckPlayerPos()
        {
            if (_player != null)
            {
                var distance = transform.position - _player.transform.position;
                if (_stateMachine.GetState() is Hunt &&
                    Mathf.Abs(distance.x) >= _distanceToLostPlayer.x ||
                    Mathf.Abs(distance.y) >= _distanceToLostPlayer.y)
                {
                    LostPlayer();
                    _minionAnimation.Lost();
                }
            }
        }

        public void SeesPlayer(Player player)
        {
            _stateMachine.ChangeState<Hunt>();
            _player = player;
        }

        public void Wait() => _stateMachine.ChangeState<Wait>();

        public void LostPlayer()
        {
            _stateMachine.ChangeState<Wait>();
            _player = null;
        }

        public void Die()
        {
            OnDieMinion?.Invoke();
            gameObject.SetActive(false);
            _stateMachine.ChangeState<Die>();
        }
    }
}