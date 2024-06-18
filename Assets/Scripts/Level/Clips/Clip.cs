using System;
using Array2DEditor;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Level.Clips
{
    public class Clip : MonoBehaviour
    {
        [SerializeField] private ClipPlace _currentClipPlace;
        [SerializeField] private bool _isCanDrag = true;
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private ClipStateEnum _clipState = ClipStateEnum.Default;
        [SerializeField] private LayerMask _playerLayer;

        [SerializeField] private bool _isEditMode;
        private bool _isBeingHeld;
        private Camera _camera;
        private Vector3 _mousePos;
        private Vector3 _startPos;

        public static event Action<Clip> OnMouseUpAction;

        public enum ClipStateEnum
        {
            Default,
            Enter,
            Exit,
            PlayerIn,
            EditMode
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
                _clipState != ClipStateEnum.PlayerIn && _isEditMode)
            {
                _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.localPosition = new Vector3(_mousePos.x - _startPos.x, _mousePos.y - _startPos.y,
                    transform.localPosition.z);
            }
        }

        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition;
                _isBeingHeld = true;
                foreach (var sprite in _spriteRenderers)
                {
                    sprite.sortingLayerName = "RoomTop";
                }
            }
        }

        private void OnMouseUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseUpAction?.Invoke(this);
                _isBeingHeld = false;
                foreach (var sprite in _spriteRenderers)
                {
                    sprite.sortingLayerName = "Room";
                }
            }
        }

        public void ChangePosition(Vector3 newPos)
        {
            transform.DOMove(newPos, 0.25f)
                .OnComplete(() => _isCanDrag = true);
        }
        
        public void PlayerEnter()
        {
            if (_clipState != ClipStateEnum.Enter
                && _clipState != ClipStateEnum.Exit)
            {
                _clipState = ClipStateEnum.PlayerIn;
            }
        }

        public void PlayerExit()
        {
            if (_clipState != ClipStateEnum.Enter
                && _clipState != ClipStateEnum.Exit)
            {
                _clipState = ClipStateEnum.Default;
            }
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
        }
    }
}