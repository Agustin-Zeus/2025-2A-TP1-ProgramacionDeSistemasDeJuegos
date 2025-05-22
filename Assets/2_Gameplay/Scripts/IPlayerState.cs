using UnityEngine;

namespace Gameplay
{
    public interface IPlayerState
    {
        void Enter();
        void Exit();
        void HandleMove(Vector2 direction);
        void HandleJump();
        void OnCollisionEnter(Collision collision);
    }
}
