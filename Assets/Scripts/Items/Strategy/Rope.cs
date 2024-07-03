using PlayerSystem;
using UnityEngine;

namespace Items.Strategy
{
    public class Rope : IStrategy
    {
        public void Use(Transform pos, IItem item, Player player)
        {
            Debug.Log("try");
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false)
        {
            item.AlternativeUse();
        }
    }
}