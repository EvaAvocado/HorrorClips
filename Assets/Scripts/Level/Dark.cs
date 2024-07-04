using System;
using Intro;
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
        [SerializeField] private Fade _fade;
        
        private const float TRANSPARENCY = 0.5f;
        private const float MAX_COLOR = 1f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                var player = other.GetComponent<Player>();
                player.IsInTheDark = true;
                
                if (player.HaveFlashlight)
                {
                    _collider2D.enabled = false;
                    _fade.FadeWithColor(new Color(0,0,0,TRANSPARENCY));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                var player = other.GetComponent<Player>();
                player.IsInTheDark = false;
                
                _fade.FadeWithColor(new Color(0,0,0,MAX_COLOR));
                _collider2D.enabled = true;
            }
        }
        
        private Color ChangeColor(float alpha) => new(0, 0, 0, alpha);
    }
}