using System;
using System.Collections.Generic;
using Level.Clips;
using UnityEngine;
using Utils;

namespace Level
{
    public class TransparentTransition : MonoBehaviour
    {
        [SerializeField] private Clip _clip;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _clipLayer;

        private List<SpriteRenderer> _leftSprites;
        private List<SpriteRenderer> _rightSprites;
        
        private const float TRANSPARENCY = 0.5f;
        private const int MAX_COLOR = 255;

        private void Awake()
        {
            _rightSprites = _clip.RightSprites;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                for (int i = 0; i < _leftSprites.Count; i++)
                {
                    _leftSprites[i].color = ChangeColor(TRANSPARENCY);
                }
                
                for (int i = 0; i < _rightSprites.Count; i++)
                {
                    _rightSprites[i].color = ChangeColor(TRANSPARENCY);
                }
            }
            
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _leftSprites = other.GetComponent<Clip>().LeftSprites;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                for (int i = 0; i < _leftSprites.Count; i++)
                {
                    _leftSprites[i].color = ChangeColor(MAX_COLOR);
                }
                
                for (int i = 0; i < _rightSprites.Count; i++)
                {
                    _rightSprites[i].color = ChangeColor(MAX_COLOR);
                }
            }
        }

        private Color ChangeColor(float alpha) => new(MAX_COLOR, MAX_COLOR, MAX_COLOR, alpha);
    }
}