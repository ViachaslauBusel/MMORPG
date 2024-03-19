using Loader;
using Protocol.Data;
using Protocol.Data.Replicated.Transform;
using RUCP;
using NetworkObjectVisualization;
using System;
using UnityEngine;
using Zenject;

namespace Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float m_speed = 6.0f;
        private IVisualRepresentation m_skinObjectHolder;
        private CharacterController m_characterController;
        private Vector3 m_moveDirection = Vector3.zero;
        private long m_lastJumpTime = 0;
        private bool m_jumped = false;
        private Animator m_animator;


        private void Awake()
        {
            m_skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
            m_characterController = GetComponent<CharacterController>();
         
        }

        private void Start()
        {
            m_skinObjectHolder.OnVisualObjectUpdated += AssignComponents;
            AssignComponents(m_skinObjectHolder.VisualObject);
        }

        private void AssignComponents(GameObject skinObject)
        {
            m_characterController = skinObject.GetComponent<CharacterController>();
            m_animator = skinObject.GetComponent<Animator>();

            enabled = m_characterController != null;
        }


        void Update()
        {
            if(m_characterController == null || m_characterController.enabled == false)
            {
                return;
            }

            if (m_characterController.isGrounded)
            {
                m_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                if (m_moveDirection.magnitude > 1.0f)
                {
                    m_moveDirection.Normalize();
                }
                m_moveDirection = m_characterController.transform.TransformDirection(m_moveDirection);
                m_moveDirection *= (m_speed * 0.95f);
            }

            // Apply gravity
            m_moveDirection.y = ApplyGravity(m_moveDirection.y);

            m_characterController.Move(m_moveDirection * Time.deltaTime);
            m_animator.SetBool("isGrounded", m_characterController.isGrounded);
        }

        private bool IsJump()
        {
            if (m_characterController.isGrounded && Input.GetAxis("Jump") > 0 && m_jumped == false)
            {
                long jumpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - m_lastJumpTime;
              
                m_jumped = jumpTime > 1_000;
                if(m_jumped)
                {
                    m_lastJumpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                }
                Debug.Log($"Local Jump:{m_jumped}");
                return m_jumped;
            }

            return false;
        }

        public bool isJumping()
        {
            return m_jumped;
        }
        private float ApplyGravity(float velocityY)
        {

            if (IsJump())
            {
                m_animator.SetTrigger("jump");
               return Config.JUMP_FORCE;
            }
            // Apply gravity
            if (m_characterController.isGrounded)
            {
               return -1f;
            }
            return Mathf.Clamp(velocityY - Config.GRAVITY_FORCE * Time.deltaTime, -Config.GRAVITY, Config.JUMP_FORCE);
        }
    
        private void OnDestroy()
        {
            m_skinObjectHolder.OnVisualObjectUpdated -= AssignComponents;
        }

        internal MoveFlag TakeFlag()
        {
            if (m_jumped)
            {
                m_jumped = false;
                return MoveFlag.Jump;
            }
            return MoveFlag.None;
        }
    }
}
