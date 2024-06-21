using Level;
using PlayerSystem;
using UnityEngine;

namespace Core
{
    public class BootstrapperOnLevel : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private EditManager _editManager;
        [SerializeField] private LayersManager _layersManager;
        [SerializeField] private MenuManager _menuManager;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _layersManager.Init();
            _editManager.Init();
            _player.OnDie += _game.ResetScene;
        }

        private void Start()
        {
            _menuManager.Init();
        }
    }
}