using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Core
{
    public class LanguageManager : MonoBehaviour
    {
        [SerializeField] private Language _currentLanguage;
        
        public enum Language
        {
            english = 0,
            russian = 1,
        }

        private void Awake()
        {
           Init();
        }

        private void Init()
        {
            if (PlayerPrefs.HasKey("Language"))
            {
                _currentLanguage = (Language)PlayerPrefs.GetInt("Language");
            }
            ChangeLanguage(_currentLanguage);
        }

        public void ChangeLanguage(Language newLanguage)
        {
            _currentLanguage = newLanguage;
            StartCoroutine(SetLocale((int)_currentLanguage));
        }
        
        IEnumerator SetLocale(int localeID)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            
            PlayerPrefs.SetInt("Language", localeID);
        }
    }
}