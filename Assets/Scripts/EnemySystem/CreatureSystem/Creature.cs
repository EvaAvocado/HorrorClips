using PlayerSystem;
using UnityEngine;

namespace EnemySystem.CreatureSystem
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private float _baseSpeed;
        [SerializeField] private float _runSpeed;

        private float _playerDirection;

        private void OnEnable()
        {
            Player.OnMove += PlayerMove;
        }

        private void OnDisable()
        {
            Player.OnMove -= PlayerMove;
        }

        private void Update()
        {
            if (_playerDirection == 0)
            {
                transform.position += transform.right * (_baseSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += transform.right * (_runSpeed * Time.deltaTime);
            }
        }

        private void PlayerMove(float direction) => _playerDirection = direction;
    }
}
