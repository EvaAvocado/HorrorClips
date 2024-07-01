using UnityEngine;
using Utils;

namespace EnemySystem.Minion
{
    public class ClipZoneFinder : MonoBehaviour
    {
        [SerializeField] private ClipZone _clipZone;
        [SerializeField] private Minion _minion;

        public Minion Minion => _minion;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_clipZone.ClipLayer.Contains(other.gameObject.layer))
            {
                _clipZone.TryChangeCollider(other);
            }
        }
    }
}