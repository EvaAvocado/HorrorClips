using System;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private Button _menuButton;
        [SerializeField] private LevelSelection _level;
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        [SerializeField] private VolumeController[] _volumeControllers;
        
        private bool _isCanOpen = true;

        public static event Action OnMenuOpen;
        public static event Action OnMenuClose;

        public void Init()
        {
            foreach (var controller in _volumeControllers)
            {
                controller.Init();
            }
            
            if (!PlayerPrefs.HasKey("IsFirstRun"))
            {
                _isCanOpen = false;
                PlayerPrefs.SetInt("IsFirstRun", 1);
            }
            else
            {
                _isCanOpen = true;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _level.gameObject.activeSelf)
            {
                _level.gameObject.SetActive(false);
                _menu.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _menu.activeSelf)
            {
                CloseMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !_menu.activeSelf)
            {
                MenuButton();
            }
        }

        public void CloseMenu()
        {
            Time.timeScale = 1;
            _menu.SetActive(false);
            OnMenuClose?.Invoke();
        }

        private void OpenMenu()
        {
            Time.timeScale = 0;
            _localizeStringEvent.OnUpdateString.Invoke(_localizeStringEvent.StringReference.GetLocalizedString());
            _menu.SetActive(true);
            OnMenuOpen?.Invoke();
        }

        public void MenuButton()
        {
            if (_isCanOpen 
                && !_menu.activeSelf)
            {
                OpenMenu();
            }
            else if (_isCanOpen 
                     && _menu.activeSelf)
            {
                CloseMenu();
            }
        }
    }
}