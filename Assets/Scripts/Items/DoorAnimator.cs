using UnityEngine;

namespace Items
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private DoorItem _door;

        public void EndAnimation()
        {
            _door.Destroy();
        }

        public void OffDoorSprite()
        {
            _door.Sprite.enabled = false;
        }
    }
}