using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ClipBackData", order = 2)]
    public class ClipBackgroundData : ScriptableObject
    {
        public Sprite wallBack;
        public Sprite wallRight;
        public Sprite floor;
    }
}
