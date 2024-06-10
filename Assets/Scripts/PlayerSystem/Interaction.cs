using Items;
using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private readonly ChangeStrategy _changeStrategy;
        private readonly Transform _hand;
        
        private IStrategy _strategy;
        private Transform _item;

        public Interaction(ChangeStrategy changeStrategy, Transform hand)
        {
            _changeStrategy = changeStrategy;
            _hand = hand;
        }
        
        public void Action()
        {
            if (_item is not null)
            {
                _strategy = _changeStrategy.SwitchStrategy(_item.GetComponent<Item>().GetItemEnum);
                _strategy?.Use(_hand, _item);
            }
        }

        public void SetItemEnum(Transform item) => _item = item;
    }
}