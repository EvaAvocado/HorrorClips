﻿using Level;
using UnityEngine;

namespace Items
{
    public class RopeAnimation : MonoBehaviour
    {
        [SerializeField] private RopeItem _ropeLeft;
        [SerializeField] private RopeItem _ropeRight;
        [SerializeField] private Animator _animator;
        [SerializeField] private ChandelierAnimation _chandelier;
        [SerializeField] private SafeAnimation _safe;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private AudioSource _audioSource;

        private SpriteRenderer _ropeLeftSprite;
        private SpriteRenderer _ropeRightSprite;
        
        private static readonly int Left = Animator.StringToHash("left");
        private static readonly int Right = Animator.StringToHash("right");

        private void Awake()
        {
            _ropeLeftSprite = _ropeLeft.GetComponent<SpriteRenderer>();
            _ropeRightSprite = _ropeRight.GetComponent<SpriteRenderer>();
        }

        private void EndLeftAnim()
        {
            _ropeLeft.ChangeSprite();
            _animator.ResetTrigger(Left);
            _chandelier.ChangeFallState();
        }

        private void EndRightAnim()
        {
            _ropeRight.ChangeSprite();
            _animator.ResetTrigger(Right);
            _safe.ChangeFallState();
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
            PlaySoundRopeCut();
            _ropeLeft.gameObject.SetActive(false);
        }
        
        private void DisableRight()
        {
            PlaySoundRopeCut();
            _ropeRight.gameObject.SetActive(false);
        }
        
        private void PlaySoundRopeCut()
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + "rope cut"));
        }
    }
}