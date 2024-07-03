using System;
using Core;
using Intro;
using UnityEngine;
using Utils;

namespace Level.Clips
{
    public class EndOpeningDoor : MonoBehaviour
    {
        [SerializeField] private bool _isEndDoor;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Fade _fade;
        [SerializeField] private Canvas _canvas;

        private void OnEnable()
        {
            _canvas.worldCamera = Camera.main;
            _canvas.sortingLayerName = "UI";
            _canvas.sortingOrder = 100;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                _fade.FadeIn();
            }
        }
    }
}