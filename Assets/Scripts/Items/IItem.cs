using UnityEngine;

namespace Items
{
    public interface IItem
    {
        bool IsDropItem();
        ItemEnum GetItemEnum();
        Transform GetTransform();
        void Drop();
        void Flip();
    }
}