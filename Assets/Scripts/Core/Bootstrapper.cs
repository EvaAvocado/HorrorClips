using PlayerSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _player.OnDie += _game.ResetScene;
        }
    }
}