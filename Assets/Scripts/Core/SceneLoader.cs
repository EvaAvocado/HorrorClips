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
        
        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }

        public void LoadSceneAfterTime()
        {
            StartCoroutine(TimerToLoadScene());
        }

        private IEnumerator TimerToLoadScene()
        {
            yield return new WaitForSeconds(_timeBeforeLoadScene);
            LoadScene();
        }
    }
}
