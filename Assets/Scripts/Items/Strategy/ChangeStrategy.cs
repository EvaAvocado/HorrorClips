using System.Collections.Generic;
using UnityEngine;

namespace Items.Strategy
{
    public class ChangeStrategy
    {
        private readonly Axe _axe;
        private readonly Door _door = new();
        private readonly Flashlight _flashlight = new();
        private readonly Rope _rope = new();
        private readonly TV _tv = new();

        public ChangeStrategy(List<Animator> animators)
        {
            _axe = new Axe(animators);
        }

        public IStrategy SwitchStrategy(ItemEnum itemEnum)
        {
            switch (itemEnum)
            {
                case ItemEnum.AXE:
                {
                    return _axe;
                }
                case ItemEnum.DOOR:
                {
                    return _door;
                }
                case ItemEnum.FLASHLIGHT:
                {
                    return _flashlight;
                }
                case ItemEnum.ROPE:
                {
                    return _rope;
                }
                case ItemEnum.TV:
                {
                    return _tv;
                }
            }

            return null;
        }
    }
}