using UnityEngine;

namespace EnemySystem
{
    public interface IStateMachine
    {
        void ChangeState<T>() where T : IEnemyState;
        void CreateStates(SpriteRenderer spriteRenderer, Transform transform, Transform playerTransform, float speed);
        void UpdateState();
        IEnemyState GetState();
    }
}