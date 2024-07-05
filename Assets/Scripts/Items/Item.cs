using System;
using EnemySystem.Minion;
using Level;
using Level.Clips;
using UnityEditorInternal;
using UnityEngine;
using Utils;

namespace Items
{
    public class Item : MonoBehaviour, IItem, ITransparent
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _leftCollider;
        [SerializeField] private Collider2D _rightCollider;
        [SerializeField] private float _speedDrop;
        [SerializeField] private ItemEnum _type;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private AudioSource _audioSource;

        private int _currentSprite;
        private EditManager _editManager;
        

        public static event Action<Item> OnAxeIdle;
        public static event Action<Item> OnAxeSpinLeft;
        public static event Action<Item> OnAxeSpinRight;
        
        private bool _isDropItem;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public bool IsDropItem() => _isDropItem;

        public ItemEnum GetItemEnum() => _type;

        private void OnEnable()
        {
            _editManager = FindObjectOfType<EditManager>();
        }

        private void Update()
        {
            if (_isDropItem)
            {
                transform.position += transform.right * (_speedDrop * Time.deltaTime);
            }
            /*else if (transform.parent is not null)
            {
                transform.localPosition = Vector3.zero;
            }*/
        }
        
        private void PlaySoundBounce()
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + "axe bounce"));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDropItem
                && _enemyLayer.Contains(other.gameObject.layer))
            {
                CheckEnemy(other);
                gameObject.SetActive(false);
            }
            
            if (_isDropItem
                && _wallLayer.Contains(other.gameObject.layer)
                && CheckWall(other))
            {
                PlaySoundBounce();
                _isDropItem = false;
                _leftCollider.enabled = false;
                _rightCollider.enabled = false;
                OnAxeIdle?.Invoke(this);
            }

            if (_clipLayer.Contains(other.gameObject.layer) && !_editManager.IsEditMode)
            {
                var clip = other.GetComponent<Clip>();
                transform.SetParent(clip.transform);
                clip.SpriteRenderers.Add(_spriteRenderer);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_clipLayer.Contains(other.gameObject.layer) && !_editManager.IsEditMode)
            {
                var clip = other.GetComponent<Clip>();
                clip.SpriteRenderers.Remove(_spriteRenderer);
            }
        }

        private bool CheckWall(Collider2D other)
        {
            if (other.TryGetComponent(out IItem door))
            {
                if (door.GetItemEnum() == ItemEnum.DOOR)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private void CheckEnemy(Collider2D other)
        {
            if (other.TryGetComponent(out ClipZoneFinder colliderInMinion))
            {
                colliderInMinion.Minion.Die();
            }
        }

        public void PlayNewAnimation(string animName)
        {
            _animator.Play(animName);
        }

        public void PlaySound(string soundName)
        {
            _audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" +soundName));
        }

        public Transform GetTransform() => transform;

        public void AlternativeUse(IItem item = null) 
        {
            if (_spriteRenderer.flipX)
            {
                OnAxeSpinLeft?.Invoke(this);
            }
            else
            {
                OnAxeSpinRight?.Invoke(this);
            }
        }

        public void Flip(float direction)
        {
            if (direction < 0
                && _speedDrop > 0)
            {
                _spriteRenderer.flipX = true;
                _speedDrop *= -1;
            }
            else if (direction > 0
                     && _speedDrop < 0)
            {
                _spriteRenderer.flipX = false;
                _speedDrop *= -1;
            }
        }

        public void ChangeSprite()
        {
            _currentSprite++;
            if (_sprites.Length < _currentSprite)
            {
                _spriteRenderer.sprite = _sprites[_currentSprite];
            }
        }

        public void Drop() => _isDropItem = true;
        public bool CheckUse(bool haveAxe)
        {
            if (_isDropItem)
            {
                return false;
            }

            return true;
        }

        public void LeftSpin() => _leftCollider.enabled = true;
        public void RightSpin() => _rightCollider.enabled = true;
    }
}