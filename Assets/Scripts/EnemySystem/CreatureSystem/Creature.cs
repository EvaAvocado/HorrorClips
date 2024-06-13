using UnityEngine;

namespace EnemySystem.CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _runSpeed;
        
        private const string HORIZONTAL = "Horizontal";
        
        private void Update()
        {
            if (Input.GetAxis(HORIZONTAL) == 0)
            {
                transform.position += transform.right * (_baseSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += transform.right * (_runSpeed * Time.deltaTime);
            }
        }
    }
}
