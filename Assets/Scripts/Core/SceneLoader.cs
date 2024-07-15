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
            _timeBeforeLoadScene = 0;
            StartCoroutine(TimerToLoadScene(sceneName));
        }

        public void LoadSceneAfterTime()
        {
            StartCoroutine(TimerToLoadScene(_sceneName));
        }

        public void ReloadScene()
        {
            _timeBeforeLoadScene = 0;
            StartCoroutine(TimerToLoadScene(SceneManager.GetActiveScene().name));
        }
        
        public void ReloadSceneAfterTime()
        {
            StartCoroutine(TimerToLoadScene(SceneManager.GetActiveScene().name));
        }

        private IEnumerator TimerToLoadScene(string sceneName)
        {
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!loadAsync.isDone)
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(_timeBeforeLoadScene);

            loadAsync.allowSceneActivation = true;
        }
    }
}
