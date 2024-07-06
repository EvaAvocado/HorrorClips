using UnityEngine;

namespace Core
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private string _soundName;
        
        public void PlaySoundByName()
        {
            _audioSource.clip = (AudioClip)Resources.Load("Sounds/" + _soundName);
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
