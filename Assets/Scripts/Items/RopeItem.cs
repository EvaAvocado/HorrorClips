﻿using UnityEngine;

namespace Items
{
    public class RopeItem : MonoBehaviour, IItem
    {
        [SerializeField] private RopeAnimation _animator;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Sprite _endSprite;
        [SerializeField] private ItemEnum _type;
        [SerializeField] private bool _isLeftRope;

        private bool _use;
        
        public bool IsDropItem()
        {
            throw new System.NotImplementedException();
        }

        public ItemEnum GetItemEnum()
        {
            return _type;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void AlternativeUse(IItem item = null)
        {
            if (_isLeftRope)
            {
                _animator.ChangeLeftState();
            }
            else
            {
                _animator.ChangeRightState();
            }

            _use = true;
        }

        public void Flip(float direction)
        {
            throw new System.NotImplementedException();
        }

        public void Drop()
        {
            throw new System.NotImplementedException();
        }

        public bool CheckUse(bool haveAxe)
        {
            if (!_use
                && haveAxe)
            {
                return true;
            }

            return false;
        }

        public void ChangeSprite()
        {
            _sprite.sprite = _endSprite;
            gameObject.SetActive(true);
        }
    }
}