using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Intro
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private float _duration = 2;
        [SerializeField] private Image _image;
        [SerializeField] private Color _color;
        [SerializeField] private UnityEvent _actionAfterFade;
    
        public float Duration
        {
            set => _duration = value;
        }
        
        public void FadeIn()
        {
            _image.DOColor(new Color(_color.r, _color.g, _color.b, 1), _duration).SetEase(Ease.Linear).OnComplete(() => _actionAfterFade?.Invoke());
        }
        
        public void FadeOut()
        {
            _image.DOColor(new Color(_color.r, _color.g, _color.b, 0), _duration).SetEase(Ease.Linear).OnComplete(() => _actionAfterFade?.Invoke());
        }
    }
}
