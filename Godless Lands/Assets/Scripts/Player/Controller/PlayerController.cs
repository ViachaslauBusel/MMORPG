using Loader;
using Protocol.Data;
using Protocol.Data.Replicated.Transform;
using RUCP;
using NetworkObjectVisualization;
using System;
using UnityEngine;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 6.0f;
        private IVisualRepresentation _skinObjectHolder;
        private CharacterController _characterController;
        private Vector2 _inputDirection = Vector2.zero;
        private long _lastJumpTime = 0;
        private Animator _animator;
        private InputManager _inputManager;
        private bool _inMove;
        private Vector3 _localDirection;

        public bool InMove => _inMove;

        public event Action OnStartMove;
        public event Action OnStopMove;
        public event Action OnJump;

        private void Awake()
        {
            _inputManager = new InputManager();
            _skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
            _characterController = GetComponent<CharacterController>();

            _inputManager.Keyboard.MoveControl.performed += MoveInput;
            _inputManager.Keyboard.MoveControl.canceled += StopMoveInput;
            _inputManager.Keyboard.Jump.performed += JumpInput;
        }

        private void OnEnable()
        {
            _inputManager.Enable();
        }
        private void OnDisable()
        {
            _inputManager.Disable();
        }

        private void Start()
        {
            _skinObjectHolder.OnVisualObjectUpdated += AssignComponents;
            AssignComponents(_skinObjectHolder.VisualObject);
        }

        private void AssignComponents(GameObject skinObject)
        {
            _characterController = skinObject.GetComponent<CharacterController>();
            _animator = skinObject.GetComponent<Animator>();

            enabled = _characterController != null;
        }

        private void JumpInput(CallbackContext context)
        {
            if (_characterController.isGrounded)
            {
                long timeSinceLastJump = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _lastJumpTime;

                if (timeSinceLastJump < 1_000) return;

                _lastJumpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                _animator.SetTrigger("jump");
                _localDirection.y = Config.JUMP_FORCE;
                OnJump?.Invoke();
            }
        }

        private void StopMoveInput(CallbackContext context)
        {
            _inputDirection = Vector2.zero;
            if (_inMove)
            {
                _inMove = false;
                OnStopMove?.Invoke();
            }
        }

        private void MoveInput(CallbackContext context)
        {
            _inputDirection = context.ReadValue<Vector2>();
            if (!_inMove)
            {
                _inMove = true;
                OnStartMove?.Invoke();
            }
        }

        void Update()
        {
            if (_characterController == null || _characterController.enabled == false)
            {
                return;
            }

            if (_characterController.isGrounded)
            {
                _localDirection = new Vector3(_inputDirection.x, _localDirection.y, _inputDirection.y);
            }
          
            Vector3 applyVelocity = _characterController.transform.TransformDirection(_localDirection);
            applyVelocity *= (_speed * 0.95f);

            // Apply gravity
            applyVelocity.y = _localDirection.y = ApplyGravity(_localDirection.y);

            _characterController.Move(applyVelocity * Time.deltaTime);
            _animator.SetBool("isGrounded", _characterController.isGrounded);
        }

       
        private float ApplyGravity(float velocityY)
        {
            // Apply gravity
            if (_characterController.isGrounded && velocityY < 0f)
            {
               return -1f;
            }
            return Mathf.Clamp(velocityY - Config.GRAVITY_FORCE * Time.deltaTime, -Config.GRAVITY, Config.JUMP_FORCE);
        }

        private void OnDestroy()
        {
            _skinObjectHolder.OnVisualObjectUpdated -= AssignComponents;

            if (_inputManager != null)

                _inputManager.Keyboard.MoveControl.performed -= MoveInput;
            _inputManager.Keyboard.MoveControl.canceled -= StopMoveInput;
            _inputManager.Keyboard.Jump.performed -= JumpInput;
        }
    }
}
