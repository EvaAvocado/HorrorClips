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

        public bool InDark
        {
            get;
            set;
        }

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
            // if (status)
            // {
            //     _player = null;
            // }
        }

        private void Awake()
        {
            _stateMachine = new EnemyStateMachine();
            _playerTransform = FindObjectOfType<Player>().transform;
            _editManager = FindObjectOfType<EditManager>();
            _stateMachine.CreateStates(_spriteRenderer, transform, _playerTransform, _speed, _editManager);
            _stateMachine.ChangeState<Wait>();
        }

        private void Update()
        {
            _stateMachine.UpdateState();

            if (_editManager.IsEditMode) //&& _stateMachine.GetState() is Hunt)
            {
                _minionAnimation.Lost();
            }
            else if (!_editManager.IsEditMode
                     && _stateMachine.GetState() is Hunt)
            {
                _minionAnimation.Hunt();
            }

            CheckPlayerPos();
        }

        private void CheckPlayerPos()
        {
            if (_player is not null)
            {
                var distance = transform.position - _player.transform.position;
                if (_stateMachine.GetState() is Hunt &&
                    Mathf.Abs(distance.y) >= _distanceToLostPlayer.y)
                {
                    LostPlayer();
                    _minionAnimation.Lost();
                    _player = null;
                    _stateMachine.ChangeState<Wait>();
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