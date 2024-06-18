using System;
using PlayerSystem;
using UnityEngine;
using Utils;

namespace Level
{
    public class Dark : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private LayerMask _playerLayer;
        
        private const float TRANSPARENCY = 0.25f;
        private const int MAX_COLOR = 255;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                CheckFlashlight(other.gameObject);
                _spriteRenderer.color = ChangeColor(TRANSPARENCY);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                _spriteRenderer.color = ChangeColor(TRANSPARENCY);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                _spriteRenderer.color = ChangeColor(MAX_COLOR);
            }
        }

        private void CheckFlashlight(GameObject player)
        {
            if (player.GetComponent<Player>().HaveFlashlight)
            {
                _collider2D.isTrigger = true;
            }
        }
        
        private Color ChangeColor(float alpha) => new(0, 0, 0, alpha);
    }
}