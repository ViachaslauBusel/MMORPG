using UnityEngine;

namespace Animation
{
    internal class CharacterMovementAnimatior : MonoBehaviour
    {
        [SerializeField]
        private float _animationSpeed = 10;
        private Animator _animator;
        private Vector3 _lastFramePosition;
        private float _forwardSpeed;
        private float _sideSpeed;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _lastFramePosition = transform.position;
        }

        private void LateUpdate()
        {
            Vector3 velocity = transform.position - _lastFramePosition;
            _lastFramePosition = transform.position;
            velocity.y = 0f;
            var speed = velocity.magnitude / Time.deltaTime;

            var direction2D = new Vector2(velocity.x, velocity.z).normalized;
            var forward2D = new Vector2(transform.forward.x, transform.forward.z).normalized;
            var right2D = new Vector2(transform.right.x, transform.right.z).normalized;
            var animationDirect =
                new Vector2(Vector2.Dot(forward2D, direction2D), Vector2.Dot(right2D, direction2D));

            var speedAnim = speed;

            _forwardSpeed = Mathf.Lerp(_forwardSpeed, animationDirect.x * speedAnim, 10 * Time.deltaTime);
            _sideSpeed = Mathf.Lerp(_sideSpeed, animationDirect.y * speedAnim, 10 * Time.deltaTime);

            _animator.SetFloat("vertical", _forwardSpeed * _animationSpeed);
            _animator.SetFloat("horizontal", _sideSpeed * _animationSpeed);
        }
    }
}
