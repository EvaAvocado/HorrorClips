using UnityEngine;

namespace Items
{
    public interface IStrategy
    {
        void Use(Transform pos, IItem item);
        void AlternativeUse(IItem item);
    }
}