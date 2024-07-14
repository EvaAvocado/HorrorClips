using System;
using DG.Tweening;
using Level;
using UnityEngine;
using UnityEngine.Rendering;

namespace Effects
{
    public class Vignette : MonoBehaviour
    {
        [SerializeField] private float _vignetteDelta = 0.388f;
        [SerializeField] private Volume _volume;

        private UnityEngine.Rendering.Universal.Vignette _vignette;
        private float _currentDelta;

        private void OnEnable()
        {
            EditManager.OnChangeEditMode += OnChangeEditMode;
        }

        private void OnDisable()
        {
            EditManager.OnChangeEditMode -= OnChangeEditMode;
        }

        private void Start()
        {
            _volume.profile.TryGet(out _vignette);
        }

        private void OnChangeEditMode(bool status)
        {
            if (status && _vignette != null)
            {
                DOTween.To(() => _currentDelta, x => _currentDelta = x, _vignetteDelta, 0.25f).SetEase(Ease.Linear);
                //_vignette.intensity.Override(_vignetteDelta);
            }
            else if (!status && _vignette != null)
            {
                DOTween.To(() => _currentDelta, x => _currentDelta = x, 0f, 0.25f).SetEase(Ease.Linear);
            }
        }

        private void Update()
        {
            _vignette.intensity.Override(_currentDelta);
        }
    }
}