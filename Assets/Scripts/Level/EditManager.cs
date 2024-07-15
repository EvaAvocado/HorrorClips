using System;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Level
{
    public class EditManager : MonoBehaviour
    {
        [SerializeField] private Animator _cinemachineAnimator;
        //[SerializeField] private CinemachineVirtualCamera _camera;
        //[SerializeField] private CinemachineVirtualCamera _cameraEditMode;
        [SerializeField] private float _orthoSize = 10;
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private GameObject _targetObjectCenter;
        [SerializeField] private bool _isEditMode;

        private bool _playerCamera;
        private bool _isCanPress = true;

        public static event Action<bool> OnChangeEditMode; 
        
        public bool IsEditMode => _isEditMode;

        public bool IsCanPress
        {
            set => _isCanPress = value;
        }

        public void Init(LayersManager layersManager)
        {
            _playerCamera = true;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space) && !_isEditMode && _isCanPress)
            {
                _isEditMode = true;
                SwitchState();
                OnChangeEditMode?.Invoke(_isEditMode);
            }
            else if (Input.GetKeyUp(KeyCode.Space) && _isEditMode && _isCanPress)
            {
                SwitchState();
                _isEditMode = false;
                OnChangeEditMode?.Invoke(_isEditMode);
            }
        }

        private void SwitchState()
        {
            if (_playerCamera)
            {
                _cinemachineAnimator.Play("EditMode");
                _playerCamera = false;
            }
            else
            {
                _cinemachineAnimator.Play("Player");
                _playerCamera = true;
            }
        }

        public void PressButton()
        {
            if (!_isEditMode && _isCanPress)
            {
                _isEditMode = true;
                SwitchState();
                OnChangeEditMode?.Invoke(_isEditMode);
            }
            else if (_isEditMode && _isCanPress)
            {
                SwitchState();
                _isEditMode = false;
                OnChangeEditMode?.Invoke(_isEditMode);
            }
        }
    }
}
