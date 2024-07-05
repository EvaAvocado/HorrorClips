using System;
using System.Collections.Generic;
using PlayerSystem;
using UnityEngine;

namespace Items.Strategy
{
    public class Axe : IStrategy
    {
        private readonly List<Animator> _animators;
        private readonly float _needTimeForThrowAxe;

        public static event Action OnSwing;
        // private static event Action OnRelease;

        public Axe(List<Animator> animators)
        {
            _animators = animators;
        }
        
        public void Use(Transform pos, IItem item, Player player)
        {
            if (!item.IsDropItem()
                && pos.childCount == 0)
            {
                //item.PlaySound("item pickup");
                item.GetTransform().parent = pos;
                item.GetTransform().localPosition = Vector3.zero;
                item.GetTransform().gameObject.SetActive(false);
                
                for (int i = 0; i < _animators.Count; i++)
                {
                    _animators[i].SetLayerWeight(1, 1f);
                }
            }
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, bool isSwing = false)
        {
            if (isSwing)
            {
                OnSwing?.Invoke();
            }
            else
            {
                item.GetTransform().gameObject.SetActive(true);
                item.GetTransform().parent = null;
                item.AlternativeUse();
            }
        }
    }
}