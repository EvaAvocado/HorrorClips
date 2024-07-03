using PlayerSystem;
using UnityEngine;

namespace Items
{
    public class FlashlightOnPlayer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        private void OnEnable()
        {
            Player.OnFlip += Flip;
        }
        
        private void OnDisable()
        {
            Player.OnFlip -= Flip;
        }

        public void OnFlashlight()
        {
            _sprite.gameObject.SetActive(true);
        }

        private void Flip()
        {
            _sprite.flipX = !_sprite.flipX;
        }
        
    }
}
