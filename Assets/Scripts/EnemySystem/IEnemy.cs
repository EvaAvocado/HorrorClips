using System;

namespace EnemySystem
{
    public interface IEnemy
    {
        public event Action OnPlayerKill;
    }
}