using System.Collections.Generic;
using Core;
using UnityEngine;

namespace UI
{
    public class SelectNewLanguageButton : MonoBehaviour
    {
        [SerializeField] private List<LanguageManager.Language> _languages;
        [SerializeField] private LanguageSelector _languageSelector;
        private int _index;

        private void Awake()
        {
            if(PlayerPrefs.HasKey("Language")) _index = PlayerPrefs.GetInt("Language");
        }

        public void SelectNewLanguage()
        {
            _index++;
            if (_index == _languages.Count) _index = 0;
            
            _languageSelector.SelectNewLanguage(_languages[_index]);
            _languageSelector.ChangeLanguage();
        }
    }
}