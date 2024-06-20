using UnityEngine;

namespace Items.Strategy
{
    public class Axe : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("Use Axe");
            if (!item.IsDropItem()
                && pos.childCount == 0)
            {
                item.GetTransform().parent = pos;
                item.GetTransform().localPosition = Vector3.zero;
            }
        }

        public void AlternativeUse(IItem item)
        {
            Debug.Log("drop");
            item.GetTransform().parent = null;
            item.AlternativeUse();
        }
    }
}