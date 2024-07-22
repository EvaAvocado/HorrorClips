using System;
using Core;
using Intro;
using Level.Clips;
using PlayerSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Level
{
    public class Dark : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Fade _fade;

        [FormerlySerializedAs("_clip")] [SerializeField]
        private Clip clipDark;

        private const float TRANSPARENCY = 0.5f;
        private const float MAX_COLOR = 1f;

        private bool _playerHasLight;
        private bool _isEditMode;
        private Clip _clipWherePlayer;

        public Clip ClipDark => clipDark;

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += ChangeEditMode;
            Player.OnHasFlashlight += ChangePlayerHasLightTrue;
            Clip.OnChangePlayerIn += ChangePlayerIn;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= ChangeEditMode;
            Player.OnHasFlashlight -= ChangePlayerHasLightTrue;
            Clip.OnChangePlayerIn -= ChangePlayerIn;
        }

        private void ChangeEditMode(bool status)
        {
            _isEditMode = status;
            if (_isEditMode)
            {
                if (clipDark.ClipState == Clip.ClipStateEnum.Enter || clipDark.ClipState == Clip.ClipStateEnum.Exit ||
                    (clipDark == _clipWherePlayer && !_playerHasLight))
                {
                    clipDark.ClipChooseSprite.FadeInSecondColorSprite();
                }

                if (clipDark == _clipWherePlayer && _playerHasLight)
                {
                    clipDark.ClipChooseSprite.FadeInThirdColorSprite();
                }

                //_collider2D.isTrigger = true;
                //_fade.FadeWithColor(new Color(0,0,0,TRANSPARENCY));
            }
            else
            {
                if (clipDark == _clipWherePlayer) return;

                //_collider2D.isTrigger = false;
                _fade.FadeWithColor(new Color(0, 0, 0, MAX_COLOR));
            }
        }

        private void ChangePlayerHasLightTrue()
        {
            _playerHasLight = true;
        }

        private void ChangePlayerIn(Clip clipIn)
        {
            _clipWherePlayer = clipIn;
        }

        public void MouseEnter()
        {
            if (_isEditMode && _playerHasLight)
            {
                if (clipDark.ClipState == Clip.ClipStateEnum.Enter ||
                    clipDark.ClipState == Clip.ClipStateEnum.Exit && clipDark != _clipWherePlayer)
                {
                    clipDark.ClipChooseSprite.FadeInThirdColorSprite();
                }

                if (clipDark != _clipWherePlayer) _fade.FadeWithColor(new Color(0, 0, 0, TRANSPARENCY));
            }
        }

        public void MouseExit()
        {
            if (clipDark.ClipState == Clip.ClipStateEnum.PlayerIn) return;

            if (_isEditMode && _playerHasLight)
            {
                if (clipDark.ClipState == Clip.ClipStateEnum.Enter ||
                    clipDark.ClipState == Clip.ClipStateEnum.Exit && clipDark != _clipWherePlayer)
                {
                    clipDark.ClipChooseSprite.FadeInSecondColorSprite();
                }
                    
                if (clipDark != _clipWherePlayer) _fade.FadeWithColor(new Color(0, 0, 0, MAX_COLOR));
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
                    _fade.FadeWithColor(new Color(0, 0, 0, TRANSPARENCY));
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

                _fade.FadeWithColor(new Color(0, 0, 0, MAX_COLOR));
                _collider2D.enabled = true;
            }
        }

        private Color ChangeColor(float alpha) => new(0, 0, 0, alpha);
    }
}