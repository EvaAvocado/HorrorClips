using UnityEngine;

namespace Core
{
    public class OpenLink : MonoBehaviour
    {
        [SerializeField] private string _link;
    
        public void Open()
        {
            Application.OpenURL(_link);
        }
    }
}
