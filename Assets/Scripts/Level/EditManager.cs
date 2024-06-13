using Cinemachine;
using DG.Tweening;
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
                _camera.Follow = null;
                DOTween.To(() => _camera.m_Lens.OrthographicSize, x=> _camera.m_Lens.OrthographicSize = x, _orthoSize * 3 + _orthoSize/2, 1f);
                _camera.transform.DOMove(new Vector3(0, _layersManager.SpawnPointY + _layersManager.Height/2 ,_camera.transform.position.z), 1f);
                _isEditMode = true;
            }
            else if (Input.GetKeyUp(KeyCode.Q) && _isEditMode)
            {
                DOTween.To(() => _camera.m_Lens.OrthographicSize, x=> _camera.m_Lens.OrthographicSize = x, _orthoSize, 1f);
                _camera.transform.DOMove(new Vector3(_targetObject.transform.position.x, _targetObject.transform.position.y , _camera.transform.position.z), 1f).OnComplete(SetCameraFollow);
                _isEditMode = false;
            }
        }

        private void SetCameraFollow()
        {
            _camera.Follow = _targetObject.transform;
        }
    }
}
