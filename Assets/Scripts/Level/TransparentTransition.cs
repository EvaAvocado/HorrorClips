using System;
using System.Collections.Generic;
using EnemySystem.CreatureSystem;
using EnemySystem.Minion;
using Level.Clips;
using PlayerSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Level
{
    public class TransparentTransition : MonoBehaviour
    {
        [SerializeField] private Clip _clip;
        [SerializeField] private Collider2D _collider;
        [FormerlySerializedAs("_playerLayer")] [SerializeField] private LayerMask _creatureLayer;
        [SerializeField] private LayerMask _clipLayer;

        private List<SpriteRenderer> _leftSprites;
        private List<SpriteRenderer> _rightSprites;
        
        private const float TRANSPARENCY = 0.5f;
        private const int MAX_COLOR = 255;
        
        public static event Action OnTransparent;
        public static event Action OnNontransparent;

        private void Awake()
        {
            _rightSprites = _clip.RightSprites;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_creatureLayer.Contains(other.gameObject.layer)
                && _leftSprites is not null && !_clip.IsEditMode)
            {
                for (int i = 0; i < _leftSprites.Count; i++)
                {
                    _leftSprites[i].color = ChangeColor(TRANSPARENCY);
                }
                
                for (int i = 0; i < _rightSprites.Count; i++)
                {
                    _rightSprites[i].color = ChangeColor(TRANSPARENCY);
                }

                if (other.TryGetComponent(out Player player))
                {
                    foreach (var sprite in player.SpriteRenderers)
                    {
                        sprite.color = ChangeColor(TRANSPARENCY);
                    }
                }

                if (other.TryGetComponent(out ClipZoneFinder minion))
                {
                    minion.Minion.SpriteRenderer.color = ChangeColor(TRANSPARENCY);
                }
                
                if (other.TryGetComponent(out Creature monster))
                {
                    monster.SpriteRenderer.color = ChangeColor(TRANSPARENCY);
                }
                
                OnTransparent?.Invoke();
            }
            
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _leftSprites = other.GetComponent<Clip>().LeftSprites;
                _collider.isTrigger = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_creatureLayer.Contains(other.gameObject.layer) && !_clip.IsEditMode)
            {
                for (int i = 0; i < _leftSprites.Count; i++)
                {
                    _leftSprites[i].color = ChangeColor(MAX_COLOR);
                }
                
                for (int i = 0; i < _rightSprites.Count; i++)
                {
                    _rightSprites[i].color = ChangeColor(MAX_COLOR);
                }
                
                if (other.TryGetComponent(out Player player))
                {
                    foreach (var sprite in player.SpriteRenderers)
                    {
                        sprite.color = ChangeColor(MAX_COLOR);
                    }
                }

                if (other.TryGetComponent(out ClipZoneFinder minion))
                {
                    minion.Minion.SpriteRenderer.color = ChangeColor(MAX_COLOR);
                }

                if (other.TryGetComponent(out Creature monster))
                {
                    monster.SpriteRenderer.color = ChangeColor(MAX_COLOR);
                }
                
                OnNontransparent?.Invoke();
            }
            
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _leftSprites = null;
                _collider.isTrigger = false;
            }
        }

        private Color ChangeColor(float alpha) => new(MAX_COLOR, MAX_COLOR, MAX_COLOR, alpha);
    }
}