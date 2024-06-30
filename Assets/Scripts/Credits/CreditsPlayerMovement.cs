using PlayerSystem;
using UnityEngine;

namespace Credits
{
    public class CreditsPlayerMovement : MonoBehaviour
    {
        [SerializeField] private bool _isPlayersControl;
        [SerializeField] private Player _player;
        
        private const string HORIZONTAL = "Horizontal";

        private void Update()
        {
            if (!_isPlayersControl)
            {
                var direction = Input.GetAxis(HORIZONTAL);
                if (direction != 0)
                {
                    _isPlayersControl = true;
                }
                
                _player.Movement.Move(1);
                _player.InvokeOnMove(1);
            }
            
        }
    }
}