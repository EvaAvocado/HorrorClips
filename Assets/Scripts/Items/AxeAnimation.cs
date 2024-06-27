using System;
using Items.Strategy;
using UnityEngine;

namespace Items
{
    public class AxeAnimation : MonoBehaviour
    {
        [SerializeField] private Item _axe;
        [SerializeField] private Animator _animator;
        
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int SpinLeft = Animator.StringToHash("spin-left");
        private static readonly int SpinRight = Animator.StringToHash("spin-right");

        private void OnEnable()
        {
            Item.OnAxeIdle += ChangeIdleState;
            Item.OnAxeSpinLeft += ChangeSpinLeftState;
            Item.OnAxeSpinRight += ChangeSpinRightState;
        }

        private void OnDisable()
        {
            Item.OnAxeIdle -= ChangeIdleState;
            Item.OnAxeSpinLeft -= ChangeSpinLeftState;
            Item.OnAxeSpinRight -= ChangeSpinRightState;
        }

        private void ChangeSpinLeftState()
        {
            _animator.ResetTrigger(Idle);
            _animator.SetTrigger(SpinLeft);
            _axe.LeftSpin();
        }
        
        private void ChangeSpinRightState()
        {
            _animator.ResetTrigger(Idle);
            _animator.SetTrigger(SpinRight);
            _axe.RightSpin();
        }

        private void ChangeIdleState()
        {
            _animator.ResetTrigger(SpinLeft);
            _animator.ResetTrigger(SpinRight);
            _animator.SetTrigger(Idle);
        }
    }
}