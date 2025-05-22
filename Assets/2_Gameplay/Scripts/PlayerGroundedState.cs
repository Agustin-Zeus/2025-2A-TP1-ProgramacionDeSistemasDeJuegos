using UnityEngine;

namespace Gameplay
{
    public class PlayerGroundedState : IPlayerState
    {
        private readonly PlayerController _controller;
        private readonly Character _character;

        public PlayerGroundedState(PlayerController controller, Character character)
        {
            _controller = controller;
            _character = character;
        }

        public void Enter() { }

        public void Exit() { }

        public void HandleMove(Vector2 direction)
        {
            _character.SetDirection(direction.ToHorizontalPlane());
        }

        public void HandleJump()
        {
            _controller.RunJumpCoroutine();
            _controller.ChangeState(_controller.JumpingState);
        }

        public void OnCollisionEnter(Collision collision) { }
    }
}
