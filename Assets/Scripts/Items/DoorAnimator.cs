using UnityEngine;

namespace Items
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private DoorItem _door;
        [SerializeField] private BoxCollider2D _colliderDoor;

        public void EndAnimation()
        {
            _door.Destroy();
        }

        public void OffDoorSprite()
        {
            _door.Sprite.enabled = false;
        }

        public void DeleteCollider()
        {
            _colliderDoor.enabled = false;
        }
    }
}