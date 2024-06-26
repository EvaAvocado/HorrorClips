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
        [SerializeField] private PitchChanger _pitchChanger;
        [SerializeField] private PressQ _pressQ;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _layersManager.Init();
            _editManager.Init(_layersManager);
            _pressQ.Init(_editManager);
            _player.OnDie += _game.ResetScene;
        }

        private void Start()
        {
            _pitchChanger.Init();
            _menuManager.Init();
        }
    }
}