using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private string _volumeParameter;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _slider;

        private float _volumeValue;
        private const float Multiplier = 20f;

        public string VolumeParameter => _volumeParameter;

        public void Init()
        {
            _slider.onValueChanged.AddListener(ValueChanged);
            
            if (PlayerPrefs.HasKey(_volumeParameter))
            {
                _volumeValue = PlayerPrefs.GetFloat(_volumeParameter, Mathf.Log10(_slider.value) * Multiplier);
                _slider.value = Mathf.Pow(10f, _volumeValue / Multiplier);
                _audioMixer.SetFloat(_volumeParameter, _volumeValue);
            }
        }

        private void ValueChanged(float value)
        {
            _volumeValue = Mathf.Log10(value) * Multiplier;
            _audioMixer.SetFloat(_volumeParameter, _volumeValue);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(_volumeParameter, _volumeValue);
        }
    }
}