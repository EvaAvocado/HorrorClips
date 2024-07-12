using PlayerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class IntroTextChooseLanguage : MonoBehaviour
    {
        [SerializeField] private bool _isRussianText;
        [SerializeField] private float _deltaScale;
        [SerializeField] private Text _text;

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
            if (_isRussianText && direction > 0 || !_isRussianText && direction < 0)
            {
                _text.transform.localScale = new Vector3(
                    _text.transform.localScale.x + (_deltaScale * Time.deltaTime),
                    _text.transform.localScale.y + (_deltaScale * Time.deltaTime), 0);
            }
            else
            {
                _text.transform.localScale = new Vector3(
                    _text.transform.localScale.x - (_deltaScale * Time.deltaTime),
                    _text.transform.localScale.y - (_deltaScale * Time.deltaTime), 0);
            }
        }
    }
}