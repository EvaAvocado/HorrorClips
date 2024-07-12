using Level;
using PlayerSystem;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core
{
    public class BootstrapperOnLevel : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private EditManager _editManager;
        [SerializeField] private LayersManager _layersManager;
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private PitchChanger _pitchChanger; 
        [FormerlySerializedAs("_pressQ")] [SerializeField] private Press _press;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private string _levelName;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _levelManager.Init();
            _layersManager.Init();
            _editManager.Init(_layersManager);
            _press.Init(_editManager);
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
            PlayerPrefs.SetInt(_levelName, 1);
            PlayerPrefs.Save();
        }
    }
}