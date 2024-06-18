using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Intro
{
    public class IntroText : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private float _deltaToShift;
        [SerializeField] private float _duration;
        
        [SerializeField] private Text _textField;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _playerLayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerLayer.Contains(other.gameObject.layer))
            {
                _collider.enabled = false;
                StartText();
            }
        }

        private void StartText()
        {
            _textField.transform.DOMove(
                new Vector3(_textField.transform.position.x, _textField.transform.position.y + _deltaToShift, _textField.transform.position.z),
                _duration);
        }

        public void SetText(String newText)
        {
            _text = newText;
            _textField.text = _text;
        }
    }
}
