using UI;
using UnityEngine;
using UnityEngine.Audio;

namespace Core
{
    public class BootstrapperIntroCredits : MonoBehaviour
    {
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _pitchParameterMusic;

        private void Start()
        {
            SetPitch(_pitchParameterMusic);
            _menuManager.Init();
        }
        
        private void SetPitch(string parameter)
        {
            _audioMixer.SetFloat(parameter, 1);
        }
    }
}
