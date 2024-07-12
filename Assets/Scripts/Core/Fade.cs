using DG.Tweening;
using Level;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private float _duration = 2;
        [SerializeField] private float _durationFadeIn;
        [SerializeField] private Image _image;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Color _color;
        [SerializeField] private Color _secondColor;
        [SerializeField] private Dark _dark;

        [FormerlySerializedAs("_actionAfterFade")] [SerializeField]
        private UnityEvent _actionAfterFadeIn;

        [SerializeField] private UnityEvent _actionAfterFadeOut;
        [SerializeField] private bool _playFadeInOnStart;
        [SerializeField] private bool _playFadeOutOnStart;

        public float Duration
        {
            set => _duration = value;
        }

        private void Start()
        {
            if (_playFadeInOnStart)
            {
                FadeIn();
            }

            if (_playFadeOutOnStart)
            {
                FadeOut();
            }
        }

        public void FadeIn()
        {
            if (_durationFadeIn != 0)
            {
                if (_image != null)
                {
                    _image.DOColor(new Color(_color.r, _color.g, _color.b, _color.a), _durationFadeIn)
                        .SetEase(Ease.Linear).OnComplete(() => _actionAfterFadeIn?.Invoke());
                }
                else
                {
                    if (_dark.gameObject.activeSelf)
                    {
                        _sprite.DOColor(new Color(_secondColor.r, _secondColor.g, _secondColor.b, _secondColor.a),
                                _durationFadeIn).SetEase(Ease.Linear)
                            .OnComplete(() => _actionAfterFadeIn?.Invoke());
                    }
                    else
                    {
                        _sprite.DOColor(new Color(_color.r, _color.g, _color.b, _color.a), _durationFadeIn)
                            .SetEase(Ease.Linear)
                            .OnComplete(() => _actionAfterFadeIn?.Invoke());
                    }
                }
            }
            else
            {
                if (_image != null)
                {
                    _image.DOColor(new Color(_color.r, _color.g, _color.b, _color.a), _duration).SetEase(Ease.Linear)
                        .OnComplete(() => _actionAfterFadeIn?.Invoke());
                }
                else
                {
                    if (_dark.gameObject.activeSelf)
                    {
                        _sprite.DOColor(new Color(_secondColor.r, _secondColor.g, _secondColor.b, _secondColor.a),
                                _duration).SetEase(Ease.Linear).OnComplete(() => _actionAfterFadeIn?.Invoke());
                    }
                    else
                    {
                        _sprite.DOColor(new Color(_color.r, _color.g, _color.b, _color.a), _duration)
                            .SetEase(Ease.Linear)
                            .OnComplete(() => _actionAfterFadeIn?.Invoke());
                    }
                }
            }
        }

        public void FadeOut()
        {
            if (_image != null)
            {
                _image.DOColor(new Color(_color.r, _color.g, _color.b, 0), _duration).SetEase(Ease.Linear)
                    .OnComplete(() => _actionAfterFadeOut?.Invoke());
            }
            else
            {
                if (_dark.gameObject.activeSelf)
                {
                    _sprite.DOColor(new Color(_secondColor.r, _secondColor.g, _secondColor.b, 0), _duration).SetEase(Ease.Linear)
                        .OnComplete(() => _actionAfterFadeOut?.Invoke());
                }
                else
                {
                    _sprite.DOColor(new Color(_color.r, _color.g, _color.b, 0), _duration).SetEase(Ease.Linear)
                        .OnComplete(() => _actionAfterFadeOut?.Invoke());
                }
            }
        }

        public void FadeWithColor(Color newColor)
        {
            if (_image != null)
            {
                _image.DOColor(newColor, _duration).SetEase(Ease.Linear);
            }
            else
            {
                _sprite.DOColor(newColor, _duration).SetEase(Ease.Linear);
            }
        }
    }
}