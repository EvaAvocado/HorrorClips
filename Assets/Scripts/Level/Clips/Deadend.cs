using UnityEngine;
using Utils;

namespace Level.Clips
{
    public class Deadend : MonoBehaviour
    {
        [SerializeField] private StopWall _stopWall;
        [SerializeField] private BoxCollider2D _collider;
        
         private void OnTriggerEnter2D(Collider2D other)
         {
             if (_stopWall.ClipLayer.Contains(other.gameObject.layer) && !_stopWall.IsDeadEnd)
             {
                 _collider.isTrigger = true;
             }
         }
        
         private void OnTriggerExit2D(Collider2D other)
         {
             if (_stopWall.ClipLayer.Contains(other.gameObject.layer) && !_stopWall.IsDeadEnd)
             {
                 _collider.isTrigger = false;
             }
        }
    }
}
