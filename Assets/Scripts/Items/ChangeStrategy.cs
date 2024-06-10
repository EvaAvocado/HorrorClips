namespace Items
{
    public class ChangeStrategy
    {
        private readonly Axe _axe = new();
        private readonly Door _doorBoard = new();

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
                    return _doorBoard;
                }
            }

            return null;
        }
    }
}