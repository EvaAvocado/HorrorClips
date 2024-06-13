using UnityEngine;

namespace EnemySystem.States
{
    public class Die : IEnemyState
    {
        private readonly GameObject _enemy;

        public Die(GameObject enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.SetActive(false);
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}