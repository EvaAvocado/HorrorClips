using UnityEngine;

namespace Items
{
    public class Axe : IStrategy
    {
        public void Use(Transform hand, Transform objectTransform)
        {
            Debug.Log("Use Axe");
            if (hand.childCount == 0)
            {
                objectTransform.parent = hand;
                objectTransform.localPosition = Vector3.zero;
            }
        }

        public void AlternativeUse(Transform objectTransform)
        {
            Debug.Log("drop");
            objectTransform.parent = null;
            objectTransform.GetComponent<IItem>().Drop();
        }
    }
}