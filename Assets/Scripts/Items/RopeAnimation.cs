using System;
using UnityEngine;

namespace Items
{
    public class RopeAnimation : MonoBehaviour
    {
        [SerializeField] private RopeItem _ropeLeft;
        [SerializeField] private RopeItem _ropeRight;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _sprite;

        private SpriteRenderer _ropeLeftSprite;
        private SpriteRenderer _ropeRightSprite;
        
        private static readonly int Left = Animator.StringToHash("left");
        private static readonly int Right = Animator.StringToHash("right");

        private void Awake()
        {
            _ropeLeftSprite = _ropeLeft.GetComponent<SpriteRenderer>();
            _ropeRightSprite = _ropeRight.GetComponent<SpriteRenderer>();
        }

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
            _sprite.sortingOrder = _ropeLeftSprite.sortingOrder;
            _animator.SetTrigger(Left);
        }

        public void ChangeRightState()
        {
            _sprite.sortingOrder = _ropeRightSprite.sortingOrder;
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