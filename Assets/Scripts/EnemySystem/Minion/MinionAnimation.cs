using System;
using UnityEngine;

namespace EnemySystem.Minion
{
    public class MinionAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _minionParent;
        [SerializeField] private GameObject _collider;
    
        private static readonly int IsHunting = Animator.StringToHash("is-hunting");
        private static readonly int IsDie = Animator.StringToHash("is-die");

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
            _collider.SetActive(false);
            _animator.SetTrigger(IsDie);
        }

        public void DieMinion()
        {
            _minionParent.gameObject.SetActive(false);
        }
    }
}
