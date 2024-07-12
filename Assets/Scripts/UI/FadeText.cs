using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FadeText : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Color _color;
        [SerializeField] private float _duration;
        
        public void FadeIn()
        {
            _text.DOColor(new Color(_color.r, _color.g, _color.b, _color.a), _duration)
                .SetEase(Ease.Linear);
        }
        
        public void FadeOut()
        {
            _text.DOColor(new Color(_color.r, _color.g, _color.b, 0), _duration)
                .SetEase(Ease.Linear);
        }
        
        public void FadeOut(Action endAction)
        {
            _text.DOColor(new Color(_color.r, _color.g, _color.b, 0), _duration)
                .SetEase(Ease.Linear).OnComplete(endAction.Invoke);
        }
    }
}
