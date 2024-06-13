using UnityEngine;

namespace Items
{
    public interface IItem
    {
        ItemEnum GetItemEnum();
        Transform GetTransform();
        void Drop();
        void Flip();
    }
}