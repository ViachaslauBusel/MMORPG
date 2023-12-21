using DynamicsObjects;
using Helpers;
using Protocol.Data.Replicated.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace DynamicsObjects.Interpolation
{
    public class PathStepJumpCalculator : PathStepCalculator
    {
        private Queue<JumpEvent> m_events = new Queue<JumpEvent>();
        private CharacterController m_characterController;
        private Animator m_animator;
        private float m_gravity = 0f;


        public PathStepJumpCalculator(ServerSettingsProvider serverSettings) : base(serverSettings)
        {
        }

        public void PushEvent(TransformEvent @event)
        {
            m_events.Enqueue(new JumpEvent()
            {
                Position = @event.Position.ToUnity(),
                EventHappenedAtVersion = @event.EventHappenedAtVersion
            });
        }

        public void SetCharacterController(CharacterController characterController)
        {
            m_characterController = characterController;
        }
        public void SetAnimator(Animator animator)
        {
            m_animator = animator;
        }

        public override float GetGravity()
        {
           
            if (m_characterController.isGrounded)
            {
                //Try make jump
                if (m_events.Count > 0)
                {
                    var jump = m_events.Peek();

                    if (jump.EmergencyApply || CheckDistance(jump.Position) || CheckDirection(jump.Position))
                    {
                        Debug.Log("Jump");
                        m_animator.SetTrigger("jump");
                        m_events.Dequeue();
                        m_gravity = Config.JUMP_FORCE * 1.1f;
                        return m_gravity;
                    }
                }

                if (m_gravity > 0f)
                { Debug.Log("Reset gravity"); }
                m_gravity = -1.0f;
            }
            else
            {
                m_gravity = Mathf.Clamp(m_gravity - Config.GRAVITY_FORCE * Time.deltaTime, -Config.GRAVITY, Config.JUMP_FORCE);
            }

            m_animator.SetBool("isGrounded", m_characterController.isGrounded);
            return m_gravity;
        }

        protected override void OnDestinationReached(Vector3 point, byte version)
        {
            foreach (var @event in m_events)
            {
                if (NumberUtils.CompareByte(version, @event.EventHappenedAtVersion) <= 0)
                {
                    Debug.Log($"Emergency apply jump:{@event.EventHappenedAtVersion} destination:{version}");
                    @event.EmergencyApply = true;
                }
            }
        }

        private bool CheckDistance(Vector3 jumpPoint)
        {
            float distanceToJumpPoint = Vector3.Distance(jumpPoint.GetClearY(), m_transform.position.GetClearY());
            return distanceToJumpPoint < 0.1f;
        }

        private bool CheckDirection(Vector3 jumpPoint)
        {
            Vector3 directionToJumpPoint = jumpPoint - m_transform.position;
            return Vector3.Dot(directionToJumpPoint.GetClearY(), m_moveDirection) < 0f;
        }
    }
}
