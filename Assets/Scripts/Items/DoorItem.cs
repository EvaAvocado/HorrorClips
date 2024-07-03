using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class DoorItem : MonoBehaviour, IItem
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private ItemEnum _type;
        [SerializeField] private Animator _animator; 

        private int _strokeCounter;
        private int _checkAnim;

        public SpriteRenderer Sprite
        {
            get => _sprite;
        }

        public static event Action OnDestroyDoor; 
        public event Action OnTouchDoor;
        
        public bool IsDropItem()
        {
            throw new System.NotImplementedException();
        }

        public ItemEnum GetItemEnum() => _type;

        public Transform GetTransform()
        {
            throw new System.NotImplementedException();
        }

        public void AlternativeUse(IItem item = null)
        {
            if (_strokeCounter < 1)
            {
                OnTouchDoor?.Invoke();
            }
            
            if (_strokeCounter < _sprites.Count)
            {
                _sprite.sprite = _sprites[_strokeCounter++];
            }
            else
            {
                if (item is not null)
                {
                    item.GetTransform().parent = null;
                    
                    _animator.gameObject.SetActive(true);
                    _animator.Play("explosion");
                    OnDestroyDoor?.Invoke();
                }
            }
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        public void Flip(float di)
        {
            throw new System.NotImplementedException();
        }

        public void Drop()
        {
            
        }

        public bool CheckUse(bool haveAxe)
        {
            return true;
        }

        public void ChangeSprite()
        {
            
        }

        public void PlayNewAnimation(string animName)
        {
            throw new NotImplementedException();
        }
    }
}