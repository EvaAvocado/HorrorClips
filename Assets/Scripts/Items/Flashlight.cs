using UnityEngine;

namespace Items
{
    public class Flashlight : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("Use Flashlight");
            if (pos.childCount == 0)
            {
                item.GetTransform().parent = pos;
                item.GetTransform().localPosition = Vector3.zero;
            }
        }

        public void AlternativeUse(IItem item)
        {
            
        }
    }
}