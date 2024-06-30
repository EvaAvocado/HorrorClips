using UnityEngine;

namespace Core
{
    public class Exit : MonoBehaviour
    {
        public void ExitFromGame()
        {
            print("Application.Quit()");
            Application.Quit();
        }
    }
}
