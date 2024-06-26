using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private LocalizeStringEvent _localizeStringEvent;
    [SerializeField] private VolumeController[] _volumeControllers;
    [SerializeField] private bool _isCanOpen = true;
    
    public static event Action OnMenuOpen; 
    public static event Action OnMenuClose; 

    public void Init()
    {
        foreach (var controller in _volumeControllers)
        {
            controller.Init();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _menu.activeSelf)
        {
            _menu.SetActive(false);
            OnMenuClose?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !_menu.activeSelf && _isCanOpen)
        {
            _localizeStringEvent.OnUpdateString.Invoke(_localizeStringEvent.StringReference.GetLocalizedString());
            _menu.SetActive(true);
            OnMenuOpen?.Invoke();
        }
    }
}
