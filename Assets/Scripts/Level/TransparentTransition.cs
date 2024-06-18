using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Level
{
    public class TransparentTransition : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _renderers;
        [SerializeField] private LayerMask _playerLayer;

        private const float TRANSPARENCY = 0.5f;
        private const int MAX_COLOR = 255;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                for (int i = 0; i < _renderers.Count; i++)
                {
                    _renderers[i].color = ChangeColor(TRANSPARENCY);
                }
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                for (int i = 0; i < _renderers.Count; i++)
                {
                    _renderers[i].color = ChangeColor(MAX_COLOR);
                }
            }
        }

        private Color ChangeColor(float alpha) => new(MAX_COLOR, MAX_COLOR, MAX_COLOR, alpha);
    }
}