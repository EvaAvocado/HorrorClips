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
    }
}