using UnityEngine;

namespace Level.Clips
{
    public class CheckerClipPlace : MonoBehaviour
    {
        [SerializeField] private Clip _clip;

        public Clip Clip => _clip;
    }
}
