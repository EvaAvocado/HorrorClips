using Core;
using Intro;
using UnityEngine;

public class OutroManager : MonoBehaviour
{
    [SerializeField] private Fade _fade;
    [SerializeField] private Animator _animator;

    public void Load10Level()
    {
        _fade.FadeIn();
    }

    public void PlayAnimation()
    {
        _animator.Play("cutscene");
    }
}
