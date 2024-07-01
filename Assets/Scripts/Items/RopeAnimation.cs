using System;
using UnityEngine;

namespace Items
{
    public class RopeAnimation : MonoBehaviour
    {
        [SerializeField] private RopeItem _ropeLeft;
        [SerializeField] private RopeItem _ropeRight;
        [SerializeField] private Animator _animator;
        
        private static readonly int Left = Animator.StringToHash("left");
        private static readonly int Right = Animator.StringToHash("right");

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void EndLeftAnim()
        {
            _ropeLeft.ChangeSprite();
            _animator.ResetTrigger(Left);
        }

        private void EndRightAnim()
        {
            _ropeRight.ChangeSprite();
            _animator.ResetTrigger(Right);
        }

        public void ChangeLeftState()
        {
            _animator.SetTrigger(Left);
        }

        public void ChangeRightState()
        {
            _animator.SetTrigger(Right);
        }

        private void DisableLeft()
        {
            _ropeLeft.gameObject.SetActive(false);
        }
        
        private void DisableRight()
        {
            _ropeRight.gameObject.SetActive(false);
        }
    }
}