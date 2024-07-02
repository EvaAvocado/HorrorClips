using UnityEngine;

namespace Level.Clips
{
    public class OnMouse : MonoBehaviour
    {
        [SerializeField] private Clip _clip;
        
        private void OnMouseDown()
        {
            _clip.MouseDown();
        }

        private void OnMouseUp()
        {
            _clip.MouseUp();
        }
    }
}