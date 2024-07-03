using PlayerSystem;
using UnityEngine;

namespace Items.Strategy
{
    public class Flashlight : IStrategy
    {
        public void Use(Transform pos, IItem item, Player player)
        {
            Debug.Log("Use Flashlight");
            /*if (pos.childCount == 0)
            {
                item.GetTransform().parent = pos;
                item.GetTransform().localPosition = Vector3.zero;
            }*/
            
            player.OnFlashlight();
            item.PlayNewAnimation("empty");
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false)
        {
            
        }
    }
}