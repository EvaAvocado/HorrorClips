using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<ITransparent> _transparentCreatures = new List<ITransparent>();

        private int _countOfCreatures;
        
        private const float TRANSPARENCY = 0.5f;
        private const int MAX_COLOR = 255;
        
        public static event Action OnTransparent;
        public static event Action OnNontransparent;

        private void Awake()
        {
            _rightSprites = _clip.RightSprites;
            _countOfCreatures = _transparentCreatures.Count;
        }

        private void OnEnable()
        {
            Minion.OnDieMinion += CheckDiedMinion;
        }
        
        private void OnDisable()
        {
            Minion.OnDieMinion -= CheckDiedMinion;
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
                    if (!_transparentCreatures.Contains(player)) _transparentCreatures.Add(player);
                    foreach (var sprite in player.SpriteRenderers)
                    {
                        sprite.color = ChangeColor(TRANSPARENCY);
                    }
                }

                if (other.TryGetComponent(out ClipZoneFinder minion))
                {
                    if (!_transparentCreatures.Contains(minion.Minion)) _transparentCreatures.Add(minion.Minion);
                    minion.Minion.SpriteRenderer.color = ChangeColor(TRANSPARENCY);
                }
                
                if (other.TryGetComponent(out Creature monster))
                {
                    if (!_transparentCreatures.Contains(monster)) _transparentCreatures.Add(monster);
                    monster.SpriteRenderer.color = ChangeColor(TRANSPARENCY);
                }

                if (_countOfCreatures != _transparentCreatures.Count)
                {
                    OnTransparent?.Invoke();
                    _countOfCreatures = _transparentCreatures.Count;
                }
            }
            
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _leftSprites = other.GetComponent<Clip>().LeftSprites;
                _collider.isTrigger = true;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
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
                    if (_transparentCreatures.Contains(player)) _transparentCreatures.Remove(player);
                    foreach (var sprite in player.SpriteRenderers)
                    {
                        sprite.color = ChangeColor(MAX_COLOR);
                    }
                }

                if (other.TryGetComponent(out ClipZoneFinder minion))
                {
                    if (_transparentCreatures.Contains(minion.Minion)) _transparentCreatures.Remove(minion.Minion);
                    minion.Minion.SpriteRenderer.color = ChangeColor(MAX_COLOR);
                }

                if (other.TryGetComponent(out Creature monster))
                {
                    if (_transparentCreatures.Contains(monster)) _transparentCreatures.Remove(monster);
                    monster.SpriteRenderer.color = ChangeColor(MAX_COLOR);
                }
                
                if (_countOfCreatures != _transparentCreatures.Count)
                {
                    OnNontransparent?.Invoke();
                    _countOfCreatures = _transparentCreatures.Count;
                }
            }
            
            if (_clipLayer.Contains(other.gameObject.layer))
            {
                _leftSprites = null;
                _collider.isTrigger = false;
            }
        }

        private void CheckDiedMinion(Minion minion)
        {
            if (_transparentCreatures.Contains(minion))
            {
                for (int i = 0; i < _leftSprites.Count; i++)
                {
                    _leftSprites[i].color = ChangeColor(MAX_COLOR);
                }
                
                for (int i = 0; i < _rightSprites.Count; i++)
                {
                    _rightSprites[i].color = ChangeColor(MAX_COLOR);
                }
                
                OnNontransparent?.Invoke();
                _transparentCreatures.Remove(minion);
            }
        }

        private Color ChangeColor(float alpha) => new(MAX_COLOR, MAX_COLOR, MAX_COLOR, alpha);
    }
}