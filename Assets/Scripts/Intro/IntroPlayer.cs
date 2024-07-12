using System;
using DG.Tweening;
using PlayerSystem;
using UnityEngine;

namespace Intro
{
    public class IntroPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _texts;
        [SerializeField] private GameObject _hints;
        [SerializeField] private Player _player;

        private bool _isCanMove = true;
        
        private void OnEnable()
        {
            Player.OnMove += Move;
        }
        
        private void OnDisable()
        {
            Player.OnMove -= Move;
        }

        private void Move(float direction)
        {
            if (!_player.IsIntro && _isCanMove)
            {
                _texts.transform.position = new Vector3(transform.position.x, _texts.transform.position.y, 0);
                _hints.transform.position = new Vector3(transform.position.x, _hints.transform.position.y, 0);
            }
            else if (_isCanMove)
            {
                _hints.transform.DOMove(new Vector3(transform.position.x - 3.75f, transform.position.y - 5f, 0), 1f);
                _isCanMove = false;
            }
            
            
        }
    }
}