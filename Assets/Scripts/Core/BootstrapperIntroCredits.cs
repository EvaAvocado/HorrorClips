using UnityEngine;

namespace Core
{
    public class BootstrapperIntroCredits : MonoBehaviour
    {
        [SerializeField] private MenuManager _menuManager;

        private void Start()
        {
            _menuManager.Init();
        }
    }
}
