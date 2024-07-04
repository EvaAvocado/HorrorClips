﻿using JetBrains.Annotations;
using PlayerSystem;
using UnityEngine;

namespace Items.Strategy
{
    public class Door : IStrategy
    {
        public void Use(Transform pos, [CanBeNull] IItem item, Player player)
        {
            Debug.Log("Tuck Tuck");
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false)
        {
            item.AlternativeUse(itemTwo);
        }
    }
}