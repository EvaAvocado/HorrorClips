using EnemySystem.CreatureSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Creature _creature;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _creature.OnPlayerKill += _game.ResetScene;
        }
    }
}