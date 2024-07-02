using System;
using System.Collections.Generic;
using Array2DEditor;
using DG.Tweening;
using EnemySystem.CreatureSystem;
using EnemySystem.Minion;
using Intro;
using UnityEngine;
using Utils;

namespace Level.Clips
{
    public class Clip : MonoBehaviour
    {
        [SerializeField] private ClipPlace _currentClipPlace;
        [SerializeField] private bool _isCanDrag = true;
        [SerializeField] private List<SpriteRenderer> _spriteRenderers;
        [SerializeField] private List<SpriteRenderer> _leftSprites;
        [SerializeField] private List<SpriteRenderer> _rightSprites;
        [SerializeField] private Collider2D _leftCollider;
        [SerializeField] private Collider2D _rightCollider;
        [SerializeField] private ClipStateEnum _clipState = ClipStateEnum.Default;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private bool _isEditMode;
        [SerializeField] private BoxCollider2D _colliderWithoutDoors;
        [SerializeField] private Fade _clipChooseSprite;

        private bool _isBeingHeld;
        private Camera _camera;
        private Vector3 _mousePos;
        private Vector3 _startPos;

        public static event Action<Clip> OnMouseUpAction;
        public static event Action OnStartMoving;
        public static event Action OnStopMoving;

        public List<SpriteRenderer> LeftSprites => _leftSprites;
        public List<SpriteRenderer> RightSprites => _rightSprites;
        public BoxCollider2D ColliderWithoutDoors => _colliderWithoutDoors;
        public bool IsEditMode => _isEditMode;

        public List<SpriteRenderer> SpriteRenderers
        {
            get => _spriteRenderers;
            set => _spriteRenderers = value;
        }

        public enum ClipStateEnum
        {
            Default,
            Enter,
            Exit,
            PlayerIn,
            MonsterIn
        }

        #region Properties

        public ClipPlace CurrentClipPlace
        {
            get => _currentClipPlace;
            set => _currentClipPlace = value;
        }

        public bool IsCanDrag
        {
            set => _isCanDrag = value;
        }

        public ClipStateEnum ClipState
        {
            set => _clipState = value;
        }

        #endregion

        private void Start()
        {
            // if (_clipState == ClipStateEnum.Enter)
            // {
            //     _leftCollider.isTrigger = false;
            // }
            // else if (_clipState == ClipStateEnum.Exit)
            // {
            //     _rightCollider.isTrigger = false;
            // }
        }

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
            _camera = Camera.main;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
        }

        private void Update()
        {
            if (_isCanDrag && _isBeingHeld && _clipState != ClipStateEnum.Enter && _clipState != ClipStateEnum.Exit &&
                _clipState != ClipStateEnum.PlayerIn && _isEditMode && _clipState != ClipStateEnum.MonsterIn)
            {
                _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.localPosition = new Vector3(_mousePos.x - _startPos.x, _mousePos.y - _startPos.y,
                    transform.localPosition.z);
            }
        }

        public void MouseDown()
        {
            if (Input.GetMouseButtonDown(0) && _isCanDrag && _isEditMode && _clipState != ClipStateEnum.Enter
                && _clipState != ClipStateEnum.Exit && _clipState != ClipStateEnum.PlayerIn &&
                _clipState != ClipStateEnum.MonsterIn)
            {
                OnStartMoving?.Invoke();
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;
                _isBeingHeld = true;
                SetSortingLayer("RoomTop");
            }
        }

        public void MouseUp()
        {
            if (Input.GetMouseButtonUp(0) && _isCanDrag && _isEditMode && _clipState != ClipStateEnum.Enter
                && _clipState != ClipStateEnum.Exit && _clipState != ClipStateEnum.PlayerIn &&
                _clipState != ClipStateEnum.MonsterIn)
            {
                OnMouseUpAction?.Invoke(this);
                OnStopMoving?.Invoke();
                _isBeingHeld = false;
            }
        }

        private void SetSortingLayer(string layerName)
        {
            foreach (var sprite in _spriteRenderers)
            {
                sprite.sortingLayerName = layerName;
            }
        }

        public void ChangePosition(Vector3 newPos)
        {
            transform.DOMove(newPos, 0.25f)
                .OnComplete(() =>
                {
                    _isCanDrag = true;
                    if (!_isBeingHeld) SetSortingLayer("Room");
                });
        }

        public void PlayerEnter()
        {
            if (_clipState != ClipStateEnum.Enter && _clipState != ClipStateEnum.Exit &&
                _clipState != ClipStateEnum.MonsterIn)
            {
                _clipState = ClipStateEnum.PlayerIn;
            }
        }

        public void PlayerExit()
        {
            if (_clipState != ClipStateEnum.Enter && _clipState != ClipStateEnum.Exit &&
                _clipState != ClipStateEnum.MonsterIn)
            {
                _clipState = ClipStateEnum.Default;
            }
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
            if (_isEditMode && (_clipState == ClipStateEnum.Enter || _clipState == ClipStateEnum.Exit ||
                                _clipState == ClipStateEnum.MonsterIn || _clipState == ClipStateEnum.PlayerIn))
            {
                _clipChooseSprite.FadeIn();
            }
            else if (!_isEditMode && (_clipState == ClipStateEnum.Enter || _clipState == ClipStateEnum.Exit ||
                                    _clipState == ClipStateEnum.MonsterIn || _clipState == ClipStateEnum.PlayerIn))
            {
                _clipChooseSprite.FadeOut();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_enemyLayer.Contains(other.gameObject.layer))
            {
                if (other.TryGetComponent(out ClipZoneFinder clipZoneFinder) && !_isEditMode)
                {
                    var minion = clipZoneFinder.Minion;
                    if (minion.ClipParent != null)
                    {
                        minion.ClipParent.SpriteRenderers.Remove(minion.SpriteRenderer);
                    }

                    minion.Parent.transform.SetParent(transform);
                    minion.ClipParent = this;

                    if (!_spriteRenderers.Contains(minion.SpriteRenderer)) _spriteRenderers.Add(minion.SpriteRenderer);
                }

                if (other.TryGetComponent(out Creature monster))
                {
                    _clipState = ClipStateEnum.MonsterIn;
                }
            }
        }
    }
}