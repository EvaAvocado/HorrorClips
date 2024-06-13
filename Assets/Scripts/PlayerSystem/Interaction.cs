using Items;
using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private readonly ChangeStrategy _changeStrategy;
        private readonly Transform _hand;
        
        private IStrategy _strategy;
        private IItem _itemInHand;
        private IItem _item;
        private bool _isAxeInHand;

        public Interaction(ChangeStrategy changeStrategy, Transform hand)
        {
            _changeStrategy = changeStrategy;
            _hand = hand;
        }
        
        public void Action()
        {
            if (_item is not null)
            {
                _strategy = _changeStrategy.SwitchStrategy(_item.GetItemEnum());
                _strategy?.Use(_hand, _item.GetTransform());
                _itemInHand = _item;
                _isAxeInHand = true;
            }
            else if (_itemInHand is not null
                && _isAxeInHand)
            {
                _strategy?.AlternativeUse(_itemInHand.GetTransform());
                _itemInHand = null;
                _isAxeInHand = false;
            }
        }

        public void SetItemEnum(IItem item) => _item = item;

        public void Flip() => _itemInHand?.Flip();
    }
}