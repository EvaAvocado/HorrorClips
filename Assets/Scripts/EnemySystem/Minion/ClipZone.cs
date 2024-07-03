﻿using System;
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
        
        private EditManager _editManager;

        public LayerMask ClipLayer => _clipLayer;

        public Clip CurrentClip => _currentClip;

        private void OnEnable()
        {
            if (_currentClip != null)
                SetNewCollider(_currentClip.transform.localPosition, _currentClip.ColliderWithoutDoors.size);
            _editManager = FindObjectOfType<EditManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_currentClip != null)
            {
                if (_playerLayer.Contains(other.gameObject.layer) && !_editManager.IsEditMode)
                {
                    _enemy.SeesPlayer(other.GetComponent<Player>());
                    _enemy.MinionAnimation.Hunt();
                }
            }
            else
            {
                if (_playerLayer.Contains(other.gameObject.layer))
                {
                    _enemy.SeesPlayer(other.GetComponent<Player>());
                    _enemy.MinionAnimation.Hunt();
                }
            }
        }

        public void TryChangeCollider(Collider2D other)
        {
            var clip = other.GetComponent<Clip>();
            if (_currentClip != clip && clip != null && !_editManager.IsEditMode)
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