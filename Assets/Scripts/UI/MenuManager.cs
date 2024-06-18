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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _menu.activeSelf)
        {
            _menu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !_menu.activeSelf)
        {
            _localizeStringEvent.OnUpdateString.Invoke(_localizeStringEvent.StringReference.GetLocalizedString());
            _menu.SetActive(true);
        }
    }
}
