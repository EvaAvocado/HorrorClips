using UnityEngine;

namespace Items.Strategy
{
    public class Rope : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("try");
        }

        public void AlternativeUse(IItem item, IItem itemTwo = null)
        {
            Debug.Log("Use Rope");
            item.GetTransform().gameObject.SetActive(false);
        }
    }
}