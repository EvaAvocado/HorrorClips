﻿using UnityEngine;

namespace Items.Strategy
{
    public interface IStrategy
    {
        void Use(Transform pos, IItem item);
        void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false);
    }
}