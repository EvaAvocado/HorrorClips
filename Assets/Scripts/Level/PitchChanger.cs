using UnityEngine;
using UnityEngine.Audio;

namespace Level
{
    public class PitchChanger : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private float _deltaToPitch = 8f;
        [SerializeField] private float _levelNumber;
        [SerializeField] private string _pitchParameterMusic;
        [SerializeField] private string _pitchParameterSound;

        private float _pitchFloat = 1f;

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
