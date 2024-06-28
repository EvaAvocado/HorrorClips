using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
