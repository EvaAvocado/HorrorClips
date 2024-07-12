using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Level
{
    public class PitchChanger : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private float _levelNumber;
        [SerializeField] private string _pitchParameterMusic;
        [SerializeField] private string _pitchParameterSound;
        [SerializeField] private bool _playOnStart;

        private float _pitchFloat = 1f;
        private float _deltaToPitch = 4.6f;

        private void Start()
        {
            if (_playOnStart)
            {
                Init();
            }
        }

        public void Init()
        {
            SetPitch(_pitchParameterMusic);
            //SetPitch(_pitchParameterSound);
        }

        private void SetPitch(string parameter)
        {
            //_audioMixer.GetFloat(parameter, out _pitchFloat);
            _audioMixer.SetFloat(parameter, _pitchFloat - (_levelNumber * _deltaToPitch)/100);
        }
    }
}
