using System;
using Items;
using Items.Strategy;
using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private readonly ChangeStrategy _changeStrategy;
        private readonly Transform _hand;
        private readonly Player _player;
        private readonly float _needTimeForThrowAxe;
        
        private IStrategy _strategy;
        private IItem _itemInHand;
        private IItem _item;
        private bool _isAxeInHand;
        private bool _isHaveFlashlight;

        public Interaction(ChangeStrategy changeStrategy, Transform hand, Player player)
        {
            _changeStrategy = changeStrategy;
            _hand = hand;
            _player = player;
        }
        
        public bool HaveFlashlight => _isHaveFlashlight;
        public bool HaveAxeInHand => _isAxeInHand;
        
        public bool Action(float pressingTime, bool isHoldAxe)
        {
            if (_item is not null)
            {
                _strategy = _changeStrategy.SwitchStrategy(_item.GetItemEnum());

                if ((_item.GetItemEnum() == ItemEnum.DOOR
                    || _item.GetItemEnum() == ItemEnum.ROPE)
                    && _isAxeInHand)
                {
                    // _strategy = _changeStrategy.SwitchStrategy(_itemInHand.GetItemEnum());
                    
                    _strategy?.AlternativeUse(_item, _itemInHand);
                    // _strategy?.AlternativeUse(_itemInHand, _item);
                    CheckAxe();
                    return true;
                }

                if (_item.GetItemEnum() == ItemEnum.TV)
                {
                    _strategy?.AlternativeUse(_item);
                }
                
                if (_item.GetItemEnum() == ItemEnum.AXE)
                {
                    _itemInHand = _item;
                    _isAxeInHand = true;
                    _strategy?.Use(_hand, _item, _player);
                    return false;
                }
                
                if (_item.GetItemEnum() == ItemEnum.FLASHLIGHT)
                {
                    _isHaveFlashlight = true;
                    _strategy?.Use(_hand, _item, _player);
                    return false;
                }

                if (_item.GetItemEnum() == ItemEnum.DOOR)
                {
                    _strategy?.Use(_hand, _item, _player);
                    return true;
                }
                
                _strategy?.Use(_hand, _item, _player);
                return false;
            }
            
            if (_itemInHand is not null 
                && _isAxeInHand)
            {
                _strategy = _changeStrategy.SwitchStrategy(_itemInHand.GetItemEnum());

                if (isHoldAxe)
                {
                    _strategy?.AlternativeUse(_itemInHand);
                }
                
                return false;
            }

            return false;
        }

        private void CheckAxe()
        {
            if (_itemInHand is not null 
                && _itemInHand.GetTransform().parent is null)
            {
                _itemInHand = null;
                _isAxeInHand = false;
            }
        }

        public void SetItem(IItem item) => _item = item;

        public void Flip(float direction) => _itemInHand?.Flip(direction);
        
        public void Drop()
        {
            if (_itemInHand is not null)
            {
                _itemInHand.Drop();
                CheckAxe();
            }
        }
    }
}