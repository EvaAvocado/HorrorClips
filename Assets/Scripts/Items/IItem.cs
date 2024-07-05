using UnityEngine;

namespace Items
{
    public interface IItem
    {
        bool IsDropItem();
        ItemEnum GetItemEnum();
        Transform GetTransform();
        void AlternativeUse(IItem item = null);
        void Flip(float direction);
        void Drop();
        bool CheckUse(bool haveAxe);
        void ChangeSprite();
        void PlayNewAnimation(string animName);
        void PlaySound(string soundName);
    }
}