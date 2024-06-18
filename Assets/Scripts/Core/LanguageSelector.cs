using UnityEngine;
using Utils;

namespace Core
{
    public class LanguageSelector : MonoBehaviour
    {
        [SerializeField] private LanguageManager _languageManager;
        [SerializeField] private LanguageManager.Language _language;

        public LanguageManager LanguageManager => _languageManager;
        public LanguageManager.Language Language => _language;

        public void ChangeLanguage()
        {
            _languageManager.ChangeLanguage(_language);
        }
        
        public void SelectNewLanguage(LanguageManager.Language language)
        {
            _language = language;
        }
    }
}