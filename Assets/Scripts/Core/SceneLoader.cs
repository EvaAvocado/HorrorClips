using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private float _timeBeforeLoadScene;
        
        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneAfterTime()
        {
            StartCoroutine(TimerToLoadScene());
        }

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void ReloadSceneAfterTime()
        {
            StartCoroutine(TimerToReloadScene());
        }

        private IEnumerator TimerToLoadScene()
        {
            yield return new WaitForSeconds(_timeBeforeLoadScene);
            LoadScene(_sceneName);
        }
        
        private IEnumerator TimerToReloadScene()
        {
            yield return new WaitForSeconds(_timeBeforeLoadScene);
            LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
