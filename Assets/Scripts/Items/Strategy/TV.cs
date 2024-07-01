using UnityEngine;

namespace Items.Strategy
{
    public class TV : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false)
        {
            item.AlternativeUse();
        }
    }
}
