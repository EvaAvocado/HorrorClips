using System;
using Level;
using Level.Clips;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Press : MonoBehaviour
{
    [SerializeField] private Color _colorCanPress;
    [SerializeField] private Color _colorCantPress;
    [SerializeField] private Image _spriteEsc;
    [SerializeField] private Button _buttonEsc;
    [SerializeField] private Image _spriteSpace;
    [SerializeField] private Button _buttonSpace;
    [SerializeField] private Image _spriteQ;
    [SerializeField] private Button _buttonQ;
    [SerializeField] private Image _spriteE;
    [SerializeField] private Button _buttonE;
    [SerializeField] private Image _spriteP;
    [SerializeField] private Button _buttonP;

    private EditManager _editManager;
    private int _transparentObjectsCount;
    private bool _isMenuOpen;
    private bool _isClipMoving;

    private void OnEnable()
    {
        TransparentTransition.OnTransparent += SetTransparent;
        TransparentTransition.OnNontransparent += SetNontransparent;
        Clip.OnStartMoving += ClipMoving;
        Clip.OnStopMoving += ClipStop;
        MenuManager.OnMenuOpen += MenuOpen;
        MenuManager.OnMenuClose += MenuClose;
    }

    private void OnDisable()
    {
        TransparentTransition.OnTransparent -= SetTransparent;
        TransparentTransition.OnNontransparent -= SetNontransparent;
        Clip.OnStartMoving -= ClipMoving;
        Clip.OnStopMoving -= ClipStop;
        MenuManager.OnMenuOpen -= MenuOpen;
        MenuManager.OnMenuClose -= MenuClose;
    }

    public void Init(EditManager editManager)
    {
        _editManager = editManager;
        SetCanPress(PressButtonEnum.Esc);
        SetCanPress(PressButtonEnum.Space);
        SetCantPress(PressButtonEnum.Q);
        SetCantPress(PressButtonEnum.E);
        SetCanPress(PressButtonEnum.P);
    }

    public void SetCanPress(PressButtonEnum type)
    {
        switch (type)
        {
            case PressButtonEnum.Esc:
            {
                _spriteEsc.color = _colorCanPress;
                _buttonEsc.interactable = true;
                break;
            }
            case PressButtonEnum.Space:
            {
                _spriteSpace.color = _colorCanPress;
                _editManager.IsCanPress = true;
                _buttonSpace.interactable = true;
                break;
            }
            case PressButtonEnum.Q:
            {
                _spriteQ.color = _colorCanPress;
                _buttonQ.interactable = true;
                break;
            }
            case PressButtonEnum.E:
            {
                _spriteE.color = _colorCanPress;
                _buttonE.interactable = true;
                break;
            }
            case PressButtonEnum.P:
            {
                _spriteP.color = _colorCanPress;
                _buttonP.interactable = true;
                break;
            }
        }
    }
    
    public void SetCantPress(PressButtonEnum type)
    {
        switch (type)
        {
            case PressButtonEnum.Esc:
            {
                _spriteEsc.color = _colorCantPress;
                _buttonEsc.interactable = false;
                break;
            }
            case PressButtonEnum.Space:
            {
                _spriteSpace.color = _colorCantPress;
                _editManager.IsCanPress = false;
                _buttonSpace.interactable = false;
                break;
            }
            case PressButtonEnum.Q:
            {
                _spriteQ.color = _colorCantPress;
                _buttonQ.interactable = false;
                break;
            }
            case PressButtonEnum.E:
            {
                _spriteE.color = _colorCantPress;
                _buttonE.interactable = false;
                break;
            }
            case PressButtonEnum.P:
            {
                _spriteP.color = _colorCantPress;
                _buttonP.interactable = false;
                break;
            }
        }
    }
    
    private void SetTransparent()
    {
        _transparentObjectsCount++;
        SetCantPress(PressButtonEnum.Space);
    }
    
    private void SetNontransparent()
    {
        _transparentObjectsCount--;
        if (_transparentObjectsCount == 0 && !_isMenuOpen && !_isClipMoving)
        {
            SetCanPress(PressButtonEnum.Space);
        }
    }

    private void MenuOpen()
    {
        _isMenuOpen = true;
        SetCantPress(PressButtonEnum.Space);
    }
    
    private void MenuClose()
    {
        _isMenuOpen = false;
        if (_transparentObjectsCount == 0 && !_isClipMoving)
        {
            SetCanPress(PressButtonEnum.Space);
        }
    }

    private void ClipMoving()
    {
        _isClipMoving = true;
        SetCantPress(PressButtonEnum.Space);
    }

    private void ClipStop()
    {
        _isClipMoving = false;
        if (_transparentObjectsCount == 0 && !_isMenuOpen)
        {
            SetCanPress(PressButtonEnum.Space);
        }
    }
}
