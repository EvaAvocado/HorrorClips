using UnityEngine;

namespace Items
{
    public class Axe : IStrategy
    {
        public void Use(Transform hand, IItem item)
        {
            Debug.Log("Use Axe");
            if (!item.IsDropItem()
                && hand.childCount == 0)
            {
                item.GetTransform().parent = hand;
                item.GetTransform().localPosition = Vector3.zero;
            }
        }

        public void AlternativeUse(IItem item)
        {
            Debug.Log("drop");
            item.GetTransform().parent = null;
            item.Drop();
        }
    }
}