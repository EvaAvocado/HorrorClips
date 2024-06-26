using System;
using Level;
using UnityEngine;
using UnityEngine.UI;

public class PressQ : MonoBehaviour
{
    [SerializeField] private Color _colorCanPress;
    [SerializeField] private Color _colorCantPress;
    [SerializeField] private Image _spriteRenderer;

    private EditManager _editManager;
    private int _transparentObjectsCount;
    private bool _isMenuOpen;

    private void OnEnable()
    {
        TransparentTransition.OnTransparent += SetTransparent;
        TransparentTransition.OnNontransparent += SetNontransparent;
        MenuManager.OnMenuOpen += MenuOpen;
        MenuManager.OnMenuClose += MenuClose;
    }

    private void OnDisable()
    {
        TransparentTransition.OnTransparent -= SetTransparent;
        TransparentTransition.OnNontransparent -= SetNontransparent;
        MenuManager.OnMenuOpen -= MenuOpen;
        MenuManager.OnMenuClose -= MenuClose;
    }

    public void Init(EditManager editManager)
    {
        _editManager = editManager;
        SetCanPress();
    }

    public void SetCanPress()
    {
        _spriteRenderer.color = _colorCanPress;
        _editManager.IsCanPress = true;
    }
    
    public void SetCantPress()
    {
        _spriteRenderer.color = _colorCantPress;
        _editManager.IsCanPress = false;
    }
    
    private void SetTransparent()
    {
        _transparentObjectsCount++;
        SetCantPress();
    }
    
    private void SetNontransparent()
    {
        _transparentObjectsCount--;
        if (_transparentObjectsCount == 0 && !_isMenuOpen)
        {
            SetCanPress();
        }
    }

    private void MenuOpen()
    {
        _isMenuOpen = true;
        SetCantPress();
    }
    
    private void MenuClose()
    {
        _isMenuOpen = false;
        if (_transparentObjectsCount == 0)
        {
            SetCanPress();
        }
    }
}
