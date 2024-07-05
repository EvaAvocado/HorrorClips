using UnityEngine;

namespace Items
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private DoorItem _door;
        [SerializeField] private BoxCollider2D _colliderDoor;
        [SerializeField] private AudioSource _audioSource;

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
        
        public void PlaySoundBroken()
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + "door break"));
        }
    }
}