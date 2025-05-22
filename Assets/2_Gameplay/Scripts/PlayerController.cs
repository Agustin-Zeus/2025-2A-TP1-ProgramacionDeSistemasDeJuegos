using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;

        //TODO: This booleans are not flexible enough. If we want to have a third jump or other things, it will become a hazzle.
        private Character _character;
        private Coroutine _jumpCoroutine;

        private IPlayerState _currentState;
        private PlayerGroundedState _groundedState;
        private PlayerJumpingState _jumpingState;
        private PlayerDoubleJumpingState _doubleJumpingState;

        public PlayerGroundedState GroundedState => _groundedState;
        public PlayerJumpingState JumpingState => _jumpingState;
        public PlayerDoubleJumpingState DoubleJumpingState => _doubleJumpingState;
        public float AirborneSpeedMultiplier => airborneSpeedMultiplier;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _groundedState = new PlayerGroundedState(this, _character);
            _jumpingState = new PlayerJumpingState(this, _character);
            _doubleJumpingState = new PlayerDoubleJumpingState(this, _character);
            _currentState = _groundedState;
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveCanceled;
            }

            if (jumpInput?.action != null)
            {
                jumpInput.action.performed += HandleJumpInput;
            }
        }

        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started -= HandleMoveInput;
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveCanceled;
            }

            if (jumpInput?.action != null)
            {
                jumpInput.action.performed -= HandleJumpInput;
            }
        }

        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            _currentState?.HandleMove(ctx.ReadValue<Vector2>());
        }

        private void HandleMoveCanceled(InputAction.CallbackContext ctx)
        {
            _currentState?.HandleMove(Vector2.zero);
        }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            _currentState?.HandleJump();
        }

        public void ChangeState(IPlayerState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void RunJumpCoroutine()
        {
            if (_jumpCoroutine != null) StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = StartCoroutine(_character.Jump());
        }

        private void OnCollisionEnter(Collision other)
        {
            _currentState.OnCollisionEnter(other);
        }
    }
}