using System.Collections.Generic;
using EnemySystem.CreatureSystem;
using EnemySystem.Minion;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Creature _creature;
        [SerializeField] private List<Minion> _minions;

        private readonly Game _game = new();
        
        private void Awake()
        {
            _creature.OnPlayerKill += _game.ResetScene;

            for (int i = 0; i < _minions.Count; i++)
            {
                _minions[i].OnPlayerKill += _game.ResetScene;
            }
        }
    }
}