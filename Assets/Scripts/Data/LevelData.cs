using UnityEngine;
using Utils;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public Array2DGameObject clips;
    }
}

