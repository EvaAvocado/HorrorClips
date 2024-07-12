using System;
using UnityEngine;

namespace Level.Clips
{
    public class OnMouse : MonoBehaviour
    {
        [SerializeField] private Clip _clip;
        [SerializeField] private Dark _dark;
        
        private void OnMouseDown()
        {
            _clip.MouseDown();
        }

        private void OnMouseUp()
        {
            _clip.MouseUp();
        }

        private void OnMouseEnter()
        {
            _dark.MouseEnter();
        }
        
        private void OnMouseExit()
        {
            _dark.MouseExit();
        }
    }
}