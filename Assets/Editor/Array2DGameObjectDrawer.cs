using Array2DEditor;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Array2DGameObject))]
    public class Array2DGameObjectDrawer : Array2DEnumDrawer<GameObject> {}
}