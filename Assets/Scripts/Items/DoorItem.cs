using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class DoorItem : MonoBehaviour, IItem
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private ItemEnum _type;

        private int _strokeCounter;
        
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
            if (_strokeCounter < _sprites.Count)
            {
                _sprite.sprite = _sprites[_strokeCounter++];
            }
            else
            {
                gameObject.SetActive(false);
                
                if (item is not null)
                {
                    item.GetTransform().parent = null;
                }
            }
        }

        public void Flip(float di)
        {
            throw new System.NotImplementedException();
        }
    }
}