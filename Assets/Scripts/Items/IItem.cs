using UnityEngine;

namespace Items
{
    public interface IItem
    {
        bool IsDropItem();
        ItemEnum GetItemEnum();
        Transform GetTransform();
        void AlternativeUse();
        void Flip();
    }
}