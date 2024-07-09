using Level;
using PlayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private LevelManager _levelManager;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _levelManager.Init();
            _layersManager.Init();
            _editManager.Init(_layersManager);
            _pressQ.Init(_editManager);
            //_player.OnDie += _layersManager.Fade.FadeIn;
            SaveLevel();
        }

        private void Start()
        {
            _pitchChanger.Init();
            _menuManager.Init();
        }

        private void SaveLevel()
        {
            PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().buildIndex}", 1);
            PlayerPrefs.Save();
        }
    }
}