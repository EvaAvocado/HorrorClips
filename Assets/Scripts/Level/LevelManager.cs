using Core;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        public void Init()
        {
            _sceneLoader = new SceneLoader();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                _sceneLoader.ReloadScene();
            }
        }
    }
}
