using Items.Strategy;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Animator _animator;
    
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int HorizontalMove = Animator.StringToHash("horizontal-move");
        private static readonly int IsThrow = Animator.StringToHash("is-throw");
        private static readonly int IsHold = Animator.StringToHash("is-hold");
        private static readonly int Release = Animator.StringToHash("release");
        private static readonly int Swing = Animator.StringToHash("swing");

        private bool _isThrow;

        private void OnEnable()
        {
            Player.OnMove += ChangeRunState;
            Player.OnIdle += ChangeIdleState;
            //Player.OnFlip += ChangeFlipState;
            Player.OnHold += ChangeHoldState;
            Player.OnThrow += ChangeThrowState;
            Player.OnRelease += ChangeReleaseState;
            Axe.OnSwing += ChangeSwingState;
        }

        private void OnDisable()
        {
            Player.OnMove -= ChangeRunState;
            Player.OnIdle -= ChangeIdleState;
            //Player.OnFlip -= ChangeFlipState;
            Player.OnHold -= ChangeHoldState;
            Player.OnThrow -= ChangeThrowState;
            Player.OnRelease -= ChangeReleaseState;
            Axe.OnSwing -= ChangeSwingState;
        }

        private void ChangeRunState(float direction)
        {
            _animator.SetBool(IsRunning, true);
            _animator.SetFloat(HorizontalMove, direction);
        }
    
        private void ChangeIdleState()
        {
            _animator.SetBool(IsRunning, false);
        }
        
        private void ChangeFlipState()
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        private void ChangeHoldState()
        {
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, true);
            
            _isThrow = true;
            _player.HoldAxe();
        }

        private void ChangeThrowState()
        {
            if (!_isThrow)
            {
                _animator.SetBool(IsThrow, true);
                _animator.SetBool(IsHold, false);
            }
        }

        private void ChangeReleaseState()
        {
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Swing);
            _animator.SetTrigger(Release);

            _isThrow = false;
        }

        private void ChangeDropAxeState()
        {
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Swing);
            _animator.ResetTrigger(Release);
            _animator.SetLayerWeight(1, 0f);
        }

        private void ChangeSwingState()
        {
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Release);
            _animator.SetTrigger(Swing);
        }
    }
}
