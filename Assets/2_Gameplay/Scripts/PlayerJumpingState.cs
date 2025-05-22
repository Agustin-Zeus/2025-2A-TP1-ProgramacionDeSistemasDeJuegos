using UnityEngine;

namespace Gameplay
{
    public class PlayerJumpingState : IPlayerState
    {
        private readonly PlayerController _controller;
        private readonly Character _character;

        public PlayerJumpingState(PlayerController controller, Character character)
        {
            _controller = controller;
            _character = character;
        }

        public void Enter() { }

        public void Exit() { }

        public void HandleMove(Vector2 direction)
        {
            _character.SetDirection(direction.ToHorizontalPlane() * _controller.AirborneSpeedMultiplier);
        }

        public void HandleJump()
        {
            _controller.RunJumpCoroutine();
            _controller.ChangeState(_controller.DoubleJumpingState);
        }

        public void OnCollisionEnter(Collision collision)
        {
            foreach (var contact in collision.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    _controller.ChangeState(_controller.GroundedState);
                }
            }
        }
    }
}
