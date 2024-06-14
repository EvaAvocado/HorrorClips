using UnityEngine;

namespace Items
{
    public interface IStrategy
    {
        void Use(Transform hand, IItem item);
        void AlternativeUse(IItem item);
    }
}