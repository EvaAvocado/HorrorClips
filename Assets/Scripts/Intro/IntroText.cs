using System;
using System.Collections;
using DG.Tweening;
using PlayerSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Intro
{
    public class IntroText : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private float _deltaToShift;

        [SerializeField] private float _deltaX = -5f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Player _player;
        [SerializeField] private Text _textField;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _playerLayer;

        [SerializeField] private UnityEvent _textArrived;
        [SerializeField] private bool _isLast;
        [SerializeField] private float _deltaScale = 5f;

        private bool _isMoving;
        private float _startPosY;
        private bool _isLastMoving;

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
            if (_isLast)
            {
                _isLastMoving = true;
                StartCoroutine(TimerToAction());
            }

            _isMoving = true;
            _startPosY = _textField.transform.position.y;

            /*_textField.transform.DOMove(
                    new Vector3(10.5f, _textField.transform.position.y + _deltaToShift, 0f), _duration)
                .OnComplete(() => _isMoving = false);*/
        }

        private void Update()
        {
            if (_isMoving && !_isLastMoving)
            {
                _textField.transform.position = new Vector3(_deltaX + _player.transform.position.x,
                    _textField.transform.position.y + (_speed * Time.deltaTime),
                    _textField.transform.position.z);

                if (_textField.transform.position.y > _deltaToShift + _startPosY)
                {
                    _isMoving = false;
                }
            }

            if (_isLastMoving)
            {
                _textField.transform.localScale = new Vector3(
                    _textField.transform.localScale.x + (_deltaScale * Time.deltaTime),
                    _textField.transform.localScale.y + (_deltaScale * Time.deltaTime),
                    _textField.transform.localScale.z + (_deltaScale * Time.deltaTime));

                if (_isMoving)
                {
                    _textField.transform.position = new Vector3(_player.transform.position.x,
                        _textField.transform.position.y + (_speed/1.75f * Time.deltaTime),
                        _textField.transform.position.z);

                    if (_textField.transform.position.y >  _player.transform.position.y)
                    {
                        _isMoving = false;
                    }
                }
                else
                {
                    _textField.transform.position = new Vector3(_player.transform.position.x,
                        _textField.transform.position.y,
                        _textField.transform.position.z);
                }
            }
        }

        public void SetText(String newText)
        {
            _text = newText;
            _textField.text = _text;
        }

        private IEnumerator TimerToAction()
        {
            yield return new WaitForSeconds(2.2f);
            _textArrived?.Invoke();
        }
    }
}