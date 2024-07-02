using System;
using Items;
using UnityEngine;

namespace Level
{
    public class Safe : MonoBehaviour
    {
        [SerializeField] private SafeAnimation _safeAnimation;
        [SerializeField] private DoorItem _rightDoor;

        private event Action OnRelease;

        private void OnEnable()
        {
            OnRelease += _safeAnimation.ChangeReleaseEnemyState;
            _rightDoor.OnTouchDoor += ReleaseEnemy;
        }

        private void OnDestroy()
        {
            OnRelease -= _safeAnimation.ChangeReleaseEnemyState;
            _rightDoor.OnTouchDoor -= ReleaseEnemy;
        }

        private void ReleaseEnemy()
        {
            OnRelease?.Invoke();
        }
    }
}