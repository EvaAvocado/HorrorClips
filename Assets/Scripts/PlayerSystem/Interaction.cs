using Items;
using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private readonly ChangeStrategy _changeStrategy;
        private readonly Transform _hand;
        private readonly Transform _head;
        
        private IStrategy _strategy;
        private IItem _itemInHand;
        private IItem _item;
        private bool _isAxeInHand;
        private bool _isHaveFlashlight;

        public Interaction(ChangeStrategy changeStrategy, Transform hand, Transform head)
        {
            _changeStrategy = changeStrategy;
            _hand = hand;
            _head = head;
        }
        
        public bool HaveFlashlight => _isHaveFlashlight;
        
        public void Action()
        {
            if (_item is not null)
            {
                _strategy = _changeStrategy.SwitchStrategy(_item.GetItemEnum());
                
                if (_item.GetItemEnum() == ItemEnum.AXE)
                {
                    _itemInHand = _item;
                    _isAxeInHand = true;
                    _strategy?.Use(_hand, _item);
                }
                else if (_item.GetItemEnum() == ItemEnum.FLASHLIGHT)
                {
                    _isHaveFlashlight = true;
                    _strategy?.Use(_head, _item);
                }
                else
                {
                    _strategy?.Use(_hand, _item);
                }
            }
            else if (_itemInHand is not null 
                     && _isAxeInHand)
            {
                _strategy?.AlternativeUse(_itemInHand);
                _itemInHand = null;
                _isAxeInHand = false;
            }
        }

        public void SetItem(IItem item) => _item = item;

        public void Flip() => _itemInHand?.Flip();
    }
}