using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemEnum _type;

        public ItemEnum GetItemEnum => _type;
    }
}