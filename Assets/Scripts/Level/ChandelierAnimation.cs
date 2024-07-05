using UnityEngine;

namespace Level
{
    public class ChandelierAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Chandelier _chandelier;
        [SerializeField] private AudioSource _audioSource;
        
        private static readonly int Fall = Animator.StringToHash("fall");

        public void ChangeFallState()
        {
            _animator.SetTrigger(Fall);
        }

        private void OnCollider()
        {
            _chandelier.EnableCollider(true);
        }

        private void OffCollider()
        {
            _chandelier.EnableCollider(false);
        }
        
        private void PlaySoundChandelier()
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + "chandelier"));
        }
    }
}