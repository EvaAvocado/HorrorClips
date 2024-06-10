using UnityEngine;

namespace Items
{
    public class Door : IStrategy
    {
        public void Use(Transform hand, Transform objectTransform)
        {
            Debug.Log("Tuck Tuck");
        }
    }
}