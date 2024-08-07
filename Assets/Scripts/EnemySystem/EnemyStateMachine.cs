﻿using System;
using System.Collections.Generic;
using EnemySystem.States;
using Level;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyStateMachine : IStateMachine
    {
        private Dictionary<Type, IEnemyState> _states;
        private IEnemyState _activeState;
        private Hunt _hunt;

        public void ChangeState<T>() where T : IEnemyState
        {
            if (_states.ContainsKey(typeof(T)))
            {
                _activeState?.Exit();
                _activeState = _states[typeof(T)];
                _activeState.Enter();
            }
            else
                throw new Exception("No such a state");
        }

        public void CreateStates(SpriteRenderer spriteRenderer, Transform transform, Transform playerTransform, float speed, EditManager editManager)
        {
            _hunt = new Hunt(spriteRenderer, transform, playerTransform, speed, editManager);
            _states = new Dictionary<Type, IEnemyState>()
            {
                [typeof(Wait)] = new Wait(),
                [typeof(Patrol)] = new Patrol(),
                [typeof(Hunt)] = _hunt,
                [typeof(Die)] = new Die(transform.gameObject)
            };
        }

        public void UpdateState() => _activeState.Update();
        
        public IEnemyState GetState() => _activeState;
        public void SetNewSpeed(float newSpeed)
        {
            _hunt.SetNewSpeed(newSpeed);
        }
    }
}