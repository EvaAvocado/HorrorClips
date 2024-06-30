using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _endPosition;
        [SerializeField] private float _timeForAnim;
        
        private void OnEnable()
        {
            AnimationText();
            transform.position = _endPosition.position;
        }

        private void AnimationText()
        {
            if (gameObject.activeSelf)
            {
                transform.DOMoveY(_startPosition.position.y, _timeForAnim).OnComplete(() =>
                {
                    transform.DOMoveY(_endPosition.position.y, _timeForAnim).OnComplete(() =>
                    {
                        AnimationText();
                    });
                });
            }
        }
    }
}