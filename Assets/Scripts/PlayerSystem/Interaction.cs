using Items;
using Items.Strategy;
using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private readonly ChangeStrategy _changeStrategy;
        private readonly Transform _hand;
        private readonly float _needTimeForThrowAxe;
        
        private IStrategy _strategy;
        private IItem _itemInHand;
        private IItem _item;
        private bool _isAxeInHand;
        private bool _isHaveFlashlight;

        public Interaction(ChangeStrategy changeStrategy, Transform hand, float needTimeForThrowAxe)
        {
            _changeStrategy = changeStrategy;
            _hand = hand;
            _needTimeForThrowAxe = needTimeForThrowAxe;
        }
        
        public bool HaveFlashlight => _isHaveFlashlight;
        
        public bool Action(float pressingTime)
        {
            if (_item is not null)
            {
                _strategy = _changeStrategy.SwitchStrategy(_item.GetItemEnum());

                if ((_item.GetItemEnum() == ItemEnum.DOOR
                    || _item.GetItemEnum() == ItemEnum.ROPE)
                    && _isAxeInHand)
                {
                    _strategy?.AlternativeUse(_item, _itemInHand);
                    CheckAxe();
                    return true;
                }
                
                if (_item.GetItemEnum() == ItemEnum.AXE)
                {
                    _itemInHand = _item;
                    _isAxeInHand = true;
                    _strategy?.Use(_hand, _item);
                    return false;
                }
                
                if (_item.GetItemEnum() == ItemEnum.FLASHLIGHT)
                {
                    _isHaveFlashlight = true;
                    _strategy?.Use(_hand, _item);
                    return false;
                }

                if (_item.GetItemEnum() == ItemEnum.DOOR)
                {
                    _strategy?.Use(_hand, _item);
                    return true;
                }
                
                _strategy?.Use(_hand, _item);
                return false;
            }
            
            if (_itemInHand is not null 
                && _isAxeInHand)
            {
                _strategy = _changeStrategy.SwitchStrategy(_itemInHand.GetItemEnum());
                _strategy?.AlternativeUse(_itemInHand, null, pressingTime);

                if (pressingTime >= _needTimeForThrowAxe)
                {
                    _itemInHand = null;
                    _isAxeInHand = false;
                }
                
                return false;
            }

            return false;
        }

        private void CheckAxe()
        {
            if (_itemInHand.GetTransform().parent is null)
            {
                _itemInHand = null;
                _isAxeInHand = false;
            }
        }

        public void SetItem(IItem item) => _item = item;

        public void Flip() => _itemInHand?.Flip();
    }
}