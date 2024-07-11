using System;
using Items;
using Items.Strategy;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;

        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int HorizontalMove = Animator.StringToHash("horizontal-move");
        private static readonly int IsThrow = Animator.StringToHash("is-throw");
        private static readonly int IsHold = Animator.StringToHash("is-hold");
        private static readonly int Release = Animator.StringToHash("release");
        private static readonly int Swing = Animator.StringToHash("swing");

        private bool _isThrow;
        private bool _isMoving;
        private bool _isCanDrop;
        private bool _isCanRelease;
        private float _direction;

        private void OnEnable()
        {
            Player.OnMove += ChangeRunState;
            Player.OnIdle += ChangeIdleState;
            //Player.OnFlip += ChangeFlipState;
            Player.OnHold += ChangeHoldState;
            Player.OnThrow += ChangeThrowState;
            Player.OnRelease += ChangeReleaseState;
            Player.OnSwing += ChangeSwingState;
            Axe.OnSwing += ChangeSwingState;
            // DoorItem.OnDestroyDoor += ChangeDropAxeState;
            DoorItem.OnDestroyDoor += DropAxe;
        }

        private void OnDisable()
        {
            Player.OnMove -= ChangeRunState;
            Player.OnIdle -= ChangeIdleState;
            //Player.OnFlip -= ChangeFlipState;
            Player.OnHold -= ChangeHoldState;
            Player.OnThrow -= ChangeThrowState;
            Player.OnRelease -= ChangeReleaseState;
            Player.OnSwing -= ChangeSwingState;
            Axe.OnSwing -= ChangeSwingState;
            // DoorItem.OnDestroyDoor -= ChangeDropAxeState;
            DoorItem.OnDestroyDoor -= DropAxe;
        }

        private void Update()
        {
            if (_isCanRelease && !Input.GetKey(KeyCode.Q))
            {
                ResetAnimator();
                _isCanRelease = false;
            }
        }

        private void ResetAnimator()
        {
            _isCanDrop = false;
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Swing);
            _animator.ResetTrigger(Release);
            _player.NotHoldAxe();
            _isThrow = false;

            if (_isMoving)
            {
                if (_direction > 0)
                {
                    _animator.Play("run_axe_right");
                }
                else
                {
                    _animator.Play("run_axe_left");
                }
            }
            else if (!_isMoving)
            {
                if (_direction > 0)
                {
                    _animator.Play("idle_axe_right");
                }
                else
                {
                    _animator.Play("idle_axe_left");
                }
            }
        }

        private void ChangeRunState(float direction)
        {
            if (!_player.IsEditMode)
            {
                _isMoving = true;
                _direction = direction;
                ResetAnimator();

                _animator.SetBool(IsRunning, true);
                _animator.SetFloat(HorizontalMove, direction);
            }
        }

        private void ChangeIdleState()
        {
            _isMoving = false;
            _animator.SetBool(IsRunning, false);
        }

        private void ChangeFlipState()
        {
            transform.localScale =
                new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }


        private void ChangeThrowState()
        {
            if (!_isThrow && !_player.IsEditMode)
            {
                _isMoving = false;
                _isCanDrop = true;
                _isCanRelease = true;
                _animator.SetBool(IsThrow, true);
                _animator.SetBool(IsHold, false);
            }
        }

        private void ChangeHoldState()
        {
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, true);

            _isThrow = true;
            _player.HoldAxe();
        }

        private void ChangeReleaseState()
        {
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Swing);
            _animator.SetTrigger(Release);

            _isThrow = false;
        }

        private void ChangeIsCanReleaseState()
        {
            _isCanRelease = false;
        }

        private void ChangeDropAxeState()
        {
            PlaySoundAxeRelease();
            
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Swing);
            _animator.ResetTrigger(Release);

            if (_isCanDrop)
            {
                _animator.SetLayerWeight(1, 0f);
                _player.DropAxe();
            }
        }

        /*private void ChangeStateForAxe()
        {
            if (_isCanDrop)
            {
                _player.DropAxe();
            }
        }*/

        private void ChangeSwingState()
        {
            _isMoving = false;
            _animator.SetBool(IsThrow, false);
            _animator.SetBool(IsHold, false);
            _animator.ResetTrigger(Release);
            _animator.SetTrigger(Swing);
        }

        private void DropAxe()
        {
            _animator.SetLayerWeight(1, 0f);
            _player.CheckAxe();
        }

        private void UseAxe()
        {
            _player.UseAxe();
        }
        
        private void PlaySoundAxeRelease()
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + "axe throw"));
        }
    }
}