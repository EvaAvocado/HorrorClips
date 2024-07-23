using DG.Tweening;
using PlayerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class IntroButton : MonoBehaviour
    {
        [SerializeField] private bool _rightArrow;
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private bool _isAlwaysInteractable;

        private void OnEnable()
        {
            Player.OnMove += SetInteractivity;
        }

        private void OnDisable()
        {
            Player.OnMove -= SetInteractivity;
        }

        private void SetInteractivity(float direction)
        {
            if (_isAlwaysInteractable)
            {
                _button.interactable = true;
                return;
            }
            
            if (direction > 0)
            {
                if (_rightArrow)
                {
                    _button.interactable = true;
                }
                else
                {
                    _button.interactable = false;
                }
            }
            else
            {
                if (!_rightArrow)
                {
                    _button.interactable = true;
                }
                else
                {
                    _button.interactable = false;
                }
            }
        }

        public void OffButton()
        {
            _button.image.DOColor(new Color(_button.image.color.r, _button.image.color.g, 
                    _button.image.color.b, 0), _duration).SetEase(Ease.Linear);

            if (_text != null)
            {
                _text.DOColor(new Color(_text.color.r, _text.color.g, 
                    _text.color.b, 0), _duration).SetEase(Ease.Linear);
            }

            if (_image != null)
            {
                _image.DOColor(new Color(_image.color.r, _image.color.g, 
                    _image.color.b, 0), _duration).SetEase(Ease.Linear);
            }
        }

        public void OnButton()
        {
            _button.image.DOColor(new Color(_button.image.color.r, _button.image.color.g, 
                _button.image.color.b, 1), _duration).SetEase(Ease.Linear);
            
            if (_text != null)
            {
                _text.DOColor(new Color(_text.color.r, _text.color.g, 
                    _text.color.b, 1), _duration).SetEase(Ease.Linear);
            }
            
            _image.DOColor(new Color(_image.color.r, _image.color.g, 
                _image.color.b, 1), _duration).SetEase(Ease.Linear);
        }
    }
}