namespace Items.Strategy
{
    public class ChangeStrategy
    {
        private readonly Axe _axe = new();
        private readonly Door _door = new();
        private readonly Flashlight _flashlight = new();
        private readonly Rope _rope = new();

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
            }

            return null;
        }
    }
}