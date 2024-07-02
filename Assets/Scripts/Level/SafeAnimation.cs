using UnityEngine;

namespace Level
{
    public class SafeAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _hole;
        [SerializeField] private GameObject _background;
        [SerializeField] private Collider2D _collider2D;
        
        private static readonly int Fall = Animator.StringToHash("fall");
        private static readonly int Property = Animator.StringToHash("enemy-exit");

        private void HoleDisable()
        {
            _hole.SetActive(false);
        }

        private void BackgroundOn()
        {
            _background.SetActive(true);
        }

        public void ChangeFallState()
        {
            _animator.SetTrigger(Fall);
        }

        public void ChangeReleaseEnemyState()
        {
            _animator.SetTrigger(Property);
            _collider2D.enabled = true;
        }
    }
}