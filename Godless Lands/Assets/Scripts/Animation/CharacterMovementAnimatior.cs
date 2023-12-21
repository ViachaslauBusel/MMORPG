using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;

namespace Animation
{
    internal class CharacterMovementAnimatior : MonoBehaviour
    {
        [SerializeField]
        private float m_animationSpeed = 10;
        private Animator m_animator;
        private Vector3 m_lastFramePosition;
        private float m_forwardSpeed;
        private float m_sideSpeed;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void Start()
        {
            m_lastFramePosition = transform.position;
        }

        private void LateUpdate()
        {
            Vector3 velocity = m_lastFramePosition - transform.position;
            m_lastFramePosition = transform.position;
            velocity.y = 0f;
            var speed = velocity.magnitude;

            var direction2D = new Vector2(velocity.x, velocity.z).normalized;
            var forward2D = new Vector2(transform.forward.x, transform.forward.z).normalized;
            var right2D = new Vector2(transform.right.x, transform.right.z).normalized;
            var animationDirect =
                new Vector2(Vector2.Dot(forward2D, direction2D), Vector2.Dot(right2D, direction2D));

            var speedAnim = speed;

            m_forwardSpeed = Mathf.Lerp(m_forwardSpeed, animationDirect.x * speedAnim, 10 * Time.deltaTime);
            m_sideSpeed = Mathf.Lerp(m_sideSpeed, animationDirect.y * speedAnim, 10 * Time.deltaTime);

            m_animator.SetFloat("vertical", -m_forwardSpeed * m_animationSpeed);
            m_animator.SetFloat("horizontal", -m_sideSpeed * m_animationSpeed);
        }
    }
}
