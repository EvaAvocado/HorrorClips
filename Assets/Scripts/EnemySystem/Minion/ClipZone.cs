using System;
using Level;
using Level.Clips;
using PlayerSystem;
using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class ClipZone : MonoBehaviour
    {
        [SerializeField] private Minion _enemy;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private LayerMask _clipLayer;
        [SerializeField] private BoxCollider2D _collider2D;
        [SerializeField] private Clip _currentClip;
        [SerializeField] private LayerMask _dark;

        private bool _isEditMode;
        private bool _inDark;

        public LayerMask ClipLayer => _clipLayer;

        public Clip CurrentClip => _currentClip;

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
            if (_currentClip != null)
                SetNewCollider(_currentClip.transform.localPosition, _currentClip.ColliderWithoutDoors.size);
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer) && !_isEditMode)
            {
                var player = other.GetComponent<Player>();

                if (player.HaveFlashlight && player.IsInTheDark || !player.IsInTheDark)
                {
                    //print("1" + player.IsInTheDark);
                    _enemy.SeesPlayer(other.GetComponent<Player>());
                    _enemy.MinionAnimation.Hunt();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_enemy.Player == null)
            {
                if (_playerLayer.Contains(other.gameObject.layer) && !_isEditMode)
                {
                    var player = other.GetComponent<Player>();

                    if (player.HaveFlashlight && player.IsInTheDark || !player.IsInTheDark)
                    {
                        //print("2" + player.IsInTheDark);
                        _enemy.SeesPlayer(other.GetComponent<Player>());
                        _enemy.MinionAnimation.Hunt();
                    }
                }
            }
        }

        public void TryChangeCollider(Collider2D other)
        {
            var clip = other.GetComponent<Clip>();
            if (_currentClip != clip && clip != null && !_isEditMode)
            {
                _currentClip = clip;
                SetNewCollider(_currentClip.transform.localPosition, _currentClip.ColliderWithoutDoors.size);
            }
        }

        public void SetNewCollider(Vector2 newPosition, Vector2 newSize)
        {
            transform.position = newPosition;
            _collider2D.size = newSize;
        }
    }
}