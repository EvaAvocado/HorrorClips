using UnityEngine;

namespace EnemySystem.Minion
{
    public class MinionAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
    
        private static readonly int IsHunting= Animator.StringToHash("is-hunting");

        public void Hunt()
        {
            _animator.SetBool(IsHunting, true);
        }

        public void Lost()
        {
            _animator.SetBool(IsHunting, false);
        }
    }
}
