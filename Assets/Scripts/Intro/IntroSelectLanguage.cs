using Core;
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
        [SerializeField] private LanguageSelector _differentLanguageSelector;
        [SerializeField] private UnityEvent _selectLanguage;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_palyerLayer.Contains(other.gameObject.layer))
            {
                _languageSelector.ChangeLanguage();
                _differentLanguageSelector.gameObject.SetActive(false);
                _selectLanguage?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}