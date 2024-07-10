using System;
using Core;
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
        
        private bool _playerHasLight;
        private bool _isEditMode;

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
            Player.OnHasFlashlight += ChangePlayerHasLightTrue;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
            Player.OnHasFlashlight -= ChangePlayerHasLightTrue;
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
            if (_isEditMode)
            {
                _collider2D.isTrigger = true;
                //_fade.FadeWithColor(new Color(0,0,0,TRANSPARENCY));
            }
            else
            {
                _collider2D.isTrigger = false;
                _fade.FadeWithColor(new Color(0,0,0,MAX_COLOR));
            }
        }

        private void ChangePlayerHasLightTrue()
        {
            _playerHasLight = true;
        }

        public void MouseEnter()
        {
            if (_isEditMode && _playerHasLight)
            {
                _fade.FadeWithColor(new Color(0,0,0,TRANSPARENCY));
            }
        }

        public void MouseExit()
        {
            if (_isEditMode && _playerHasLight)
            {
                _fade.FadeWithColor(new Color(0,0,0,MAX_COLOR));
            }
        }

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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                var player = other.GetComponent<Player>();
                player.IsInTheDark = true;
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