using UnityEngine;

namespace Credits
{
    public class PlayAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animName;

        private void Start()
        {
            _animator.Play(_animName);
        }
    }
}
