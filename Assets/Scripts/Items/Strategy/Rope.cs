using UnityEngine;

namespace Items.Strategy
{
    public class Rope : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("try");
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false)
        {
            Debug.Log("Use Rope");
            item.AlternativeUse();
        }
    }
}