using UnityEngine;

namespace Items
{
    public interface IStrategy
    {
        void Use(Transform hand, Transform objectTransform);
    }
}