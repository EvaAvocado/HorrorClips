using UnityEngine;

namespace Items
{
    public class Door : IStrategy
    {
        public void Use(Transform pos, IItem item)
        {
            Debug.Log("Tuck Tuck");
        }

        public void AlternativeUse(IItem item)
        {
            Debug.Log("Try open");
        }
    }
}