using System.Collections.Generic;
using UnityEngine;

namespace Items.Strategy
{
    public class Axe : IStrategy
    {
        private List<Animator> _animators;
        
        public Axe(List<Animator> animators)
        {
            _animators = animators;
        }
        
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("Use Axe");
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

        public void AlternativeUse(IItem item, IItem itemTwo = null)
        {
            Debug.Log("drop");
            item.GetTransform().gameObject.SetActive(true);
            item.GetTransform().parent = null;
            item.AlternativeUse();
            
            for (int i = 0; i < _animators.Count; i++)
            {
                _animators[i].SetLayerWeight(1, 0f);
            }
        }
    }
}