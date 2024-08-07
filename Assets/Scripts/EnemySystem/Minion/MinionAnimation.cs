using System;
using UnityEngine;

namespace EnemySystem.Minion
{
    public class MinionAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _minionParent;
        [SerializeField] private GameObject _collider;
        [SerializeField] private Animator _dieAnimator;
        [SerializeField] private SpriteRenderer _spriteMinion;
        [SerializeField] private Minion _minion;
        [SerializeField] private AudioSource _audioSource;
    
        private static readonly int IsHunting = Animator.StringToHash("is-hunting");
        public static event Action<Minion> OnDieMinionPlaySound;
        
        
        private void OnEnable()
        {
            Minion.OnDieMinion += DieAnimation;
        }

        private void OnDisable()
        {
            Minion.OnDieMinion -= DieAnimation;
        }

        public void Hunt()
        {
            _animator.SetBool(IsHunting, true);
        }

        public void Lost()
        {
            _animator.SetBool(IsHunting, false);
        }
        
        private void DieAnimation(Minion minion)
        {
            PlaySoundDie();
            if (minion == _minion)
            {
                _collider.SetActive(false);
                _minion.StateMachine.SetNewSpeed(0);
                //_animator.speed = 0;
                _dieAnimator.Play("die");
            }
        }
        
        private void PlaySoundDie()
        {
            OnDieMinionPlaySound?.Invoke(_minion);
            /*_audioSource.clip = (AudioClip)Resources.Load("Sounds/" + "wilhelm-1-86895");
            _audioSource.PlayOneShot(_audioSource.clip);*/
        }

        public void OffMinionSprite()
        {
            _spriteMinion.enabled = false;
        }

        public void DieMinion()
        {
            _minionParent.gameObject.SetActive(false);
        }
    }
}
