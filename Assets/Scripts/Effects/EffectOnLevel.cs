using UnityEngine;
using UnityEngine.Rendering;

namespace Effects
{
    // пока не реализован
    public class EffectOnLevel : MonoBehaviour
    {
        [SerializeField] private float _vignetteDelta = 0.388f;
        [SerializeField] private Volume _volume;
    
        private UnityEngine.Rendering.Universal.FilmGrain _filmGrain;
        //private UnityEngine.Rendering.Universal. _filmGrain; 1 0.295 0.756 
    }
}
