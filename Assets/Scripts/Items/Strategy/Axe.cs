using System.Collections.Generic;
using UnityEngine;

namespace Items.Strategy
{
    public class Axe : IStrategy
    {
        private readonly List<Animator> _animators;
        private readonly float _needTimeForThrowAxe;
        
        private static readonly int Swing = Animator.StringToHash("swing");

        public Axe(List<Animator> animators, float needTimeForThrowAxe)
        {
            _animators = animators;
            _needTimeForThrowAxe = needTimeForThrowAxe;
        }
        
        public void Use(Transform pos, IItem item)
        {
            if (!item.IsDropItem()
                && pos.childCount == 0)
            {
                item.GetTransform().parent = pos;
                item.GetTransform().localPosition = Vector3.zero;
                item.GetTransform().gameObject.SetActive(false);
                
                for (int i = 0; i < _animators.Count; i++)
                {
                    _animators[i].SetLayerWeight(1, 1f);
                }
            }
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null, float pressingTime = 0)
        {
            if (pressingTime < _needTimeForThrowAxe)
            {
                for (int i = 0; i < _animators.Count; i++)
                {
                    _animators[i].SetTrigger(Swing);
                }
            }
            else
            {
                item.GetTransform().gameObject.SetActive(true);
                item.GetTransform().parent = null;
                item.AlternativeUse();
                
                for (int i = 0; i < _animators.Count; i++)
                {
                    _animators[i].SetLayerWeight(1, 0f);
                    _animators[i].ResetTrigger(Swing);
                }
            }
        }
    }
}