using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Outro
{
    public class OutroManager : MonoBehaviour
    {
        [SerializeField] private Fade _fade;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private UnityEvent _actionIfVideoEnd;

        private bool _isEnd;
       
        void OnEnable()
        {
            _videoPlayer.loopPointReached += OnVideoEnd;
        }

        void OnDisable()
        {
            _videoPlayer.loopPointReached -= OnVideoEnd;
        }

        void OnVideoEnd(VideoPlayer causedVideoPlayer)
        {
            if (!_isEnd)
            {
                print("THE END");
                _actionIfVideoEnd?.Invoke();
                _isEnd = true;
            }
        }
    }
}