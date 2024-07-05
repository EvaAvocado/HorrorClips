using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        public void Init()
        {
            print("start");
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                SceneManager.LoadScene((SceneManager.GetActiveScene().name));
            }
        }
    }
}
