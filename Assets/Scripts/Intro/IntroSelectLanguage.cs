using Core;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Intro
{
    public class IntroSelectLanguage : MonoBehaviour
    {
        [SerializeField] private LanguageSelector _languageSelector;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private LayerMask _palyerLayer;
        [SerializeField] private IntroSelectLanguage _differentIntroSelectLanguage;
        [SerializeField] private UnityEvent _selectLanguage;
        [SerializeField] private FadeText _fadeText;

        public FadeText FadeText => _fadeText;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_palyerLayer.Contains(other.gameObject.layer))
            {
                _languageSelector.ChangeLanguage();
                _differentIntroSelectLanguage.FadeText.FadeOut();
                _selectLanguage?.Invoke();
                _fadeText.FadeOut();
            }
        }
    }
}