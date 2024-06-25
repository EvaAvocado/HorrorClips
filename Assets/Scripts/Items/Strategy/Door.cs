using UnityEngine;

namespace Items.Strategy
{
    public class Door : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("Tuck Tuck");
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, float pressingTime = 0)
        {
            Debug.Log("Try open");
            item.AlternativeUse(itemTwo);
        }
    }
}