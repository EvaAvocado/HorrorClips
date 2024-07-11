using System;
using System.Collections;
using EnemySystem.States;
using Level;
using Level.Clips;
using PlayerSystem;
using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class Minion : MonoBehaviour, ITransparent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _speed;
        [SerializeField] private LayerMask _axeLayer;
        [SerializeField] private GameObject _parent;
        [SerializeField] private MinionAnimation _minionAnimation;
        [SerializeField] private Vector2 _distanceToLostPlayer;
        [SerializeField] private AudioSource _audioSource;

        private Clip _clipParent;

        private IStateMachine _stateMachine;
        private Player _player;
        private EditManager _editManager;
        private bool _isCanPlaySound = true;
        
        public static event Action<Minion> OnDieMinion;

        public GameObject Parent => _parent;
        public MinionAnimation MinionAnimation => _minionAnimation;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public Player Player => _player;

        public float Speed
        {
            set => _speed = value;
        }

        public Clip ClipParent
        {
            get => _clipParent;
            set => _clipParent = value;
        }

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
        }

        private void ChangeEditMode(bool status)
        {
            if (status)
            {
                _player = null;
            }
        }

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

            if (_editManager.IsEditMode) //&& _stateMachine.GetState() is Hunt)
            {
                LostPlayer();
                _minionAnimation.Lost();
            }

            CheckPlayerPos();
        }

        private void CheckPlayerPos()
        {
            if (_player is not null)
            {
                var distance = transform.position - _player.transform.position;
                if (_stateMachine.GetState() is Hunt &&
                    Mathf.Abs(distance.x) >= _distanceToLostPlayer.x ||
                    Mathf.Abs(distance.y) >= _distanceToLostPlayer.y)
                {
                    LostPlayer();
                    _minionAnimation.Lost();
                    _player = null;
                }
            }
        }

        public void SeesPlayer(Player player)
        {
            if (_isCanPlaySound)
            {
                PlaySoundSpot();
                _isCanPlaySound = false;
            }
            _stateMachine.ChangeState<Hunt>();
            _player = player;
        }

        public void Wait() => _stateMachine.ChangeState<Wait>();

        public void LostPlayer()
        {
            _isCanPlaySound = true;
            _stateMachine.ChangeState<Wait>();
            _player = null;
        }

        public void Die()
        {
            OnDieMinion?.Invoke(this);
            _stateMachine.ChangeState<Die>();
        }
        
        private void PlaySoundSpot()
        {
            _audioSource.clip = (AudioClip)Resources.Load("Sounds/" + "minion spot");
            _audioSource.PlayOneShot(_audioSource.clip );
        }
        
    }
}