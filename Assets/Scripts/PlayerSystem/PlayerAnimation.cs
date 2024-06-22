using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
    
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int HorizontalMove = Animator.StringToHash("horizontal-move");

        private void OnEnable()
        {
            Player.OnMove += ChangeRunState;
            Player.OnIdle += ChangeIdleState;
            Player.OnFlip += ChangeFlipState;
        }

        private void OnDisable()
        {
            Player.OnMove -= ChangeRunState;
            Player.OnIdle -= ChangeIdleState;
            Player.OnFlip -= ChangeFlipState;
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
    }
}
