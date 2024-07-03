using PlayerSystem;
using UnityEngine;

namespace Items.Strategy
{
    public interface IStrategy
    {
        void Use(Transform pos, IItem item, Player player);
        void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false);
    }
}