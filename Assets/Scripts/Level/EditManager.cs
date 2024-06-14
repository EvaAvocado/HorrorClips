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
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float _orthoSize = 10;
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private bool _isEditMode;
        [SerializeField] private LayersManager _layersManager;
        private TweenerCore<float, float, FloatOptions> _currentTween;

        public static event Action<bool> OnChangeEditMode; 
        
        public bool IsEditMode => _isEditMode;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _targetObject = _camera.Follow.gameObject;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q) && !_isEditMode)
            {
                if (_currentTween != null)
                {
                    DOTween.Kill(_currentTween);
                    _camera.transform.DOKill();
                }
                
                _camera.Follow = null;
                _currentTween = DOTween.To(() => _camera.m_Lens.OrthographicSize, x=> _camera.m_Lens.OrthographicSize = x, _orthoSize * 3 + _orthoSize/2, 1f);
                _camera.transform.DOMove(new Vector3(0, _layersManager.SpawnPointY + _layersManager.Height/2 ,_camera.transform.position.z), 1f);
                _isEditMode = true;
                OnChangeEditMode?.Invoke(_isEditMode);
            }
            else if (Input.GetKeyUp(KeyCode.Q) && _isEditMode)
            {
                if (_currentTween != null)
                {
                    DOTween.Kill(_currentTween);
                    _camera.transform.DOKill();
                }
                
                _currentTween = DOTween.To(() => _camera.m_Lens.OrthographicSize, x=> _camera.m_Lens.OrthographicSize = x, _orthoSize, 1f);
                _camera.transform.DOMove(new Vector3(_targetObject.transform.position.x, _targetObject.transform.position.y , _camera.transform.position.z), 1f).OnComplete(SetCameraFollow);
                _isEditMode = false;
                OnChangeEditMode?.Invoke(_isEditMode);
            }
        }

        private void SetCameraFollow()
        {
            _camera.Follow = _targetObject.transform;
        }
    }
}
