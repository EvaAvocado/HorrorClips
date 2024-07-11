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
    [SerializeField] private Image _spriteSpace;
    [SerializeField] private Image _spriteQ;
    [SerializeField] private Image _spriteE;
    [SerializeField] private Image _spriteP;

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
                break;
            }
            case PressButtonEnum.Space:
            {
                _spriteSpace.color = _colorCanPress;
                _editManager.IsCanPress = true;
                break;
            }
            case PressButtonEnum.Q:
            {
                _spriteQ.color = _colorCanPress;
                break;
            }
            case PressButtonEnum.E:
            {
                _spriteE.color = _colorCanPress;
                break;
            }
            case PressButtonEnum.P:
            {
                _spriteP.color = _colorCanPress;
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
                break;
            }
            case PressButtonEnum.Space:
            {
                _spriteSpace.color = _colorCantPress;
                _editManager.IsCanPress = false;
                break;
            }
            case PressButtonEnum.Q:
            {
                _spriteQ.color = _colorCantPress;
                break;
            }
            case PressButtonEnum.E:
            {
                _spriteE.color = _colorCantPress;
                break;
            }
            case PressButtonEnum.P:
            {
                _spriteP.color = _colorCantPress;
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
