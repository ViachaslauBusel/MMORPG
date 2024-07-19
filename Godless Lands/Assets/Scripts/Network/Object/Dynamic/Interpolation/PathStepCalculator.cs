using UnityEngine;

namespace Network.Object.Dynamic.Interpolation
{
    public class PathStepCalculator
    {
        protected ServerSettingsProvider m_serverSettings;
        protected Transform m_transform;
        protected Vector3 m_targetPoint;
        protected byte m_targetVersion;
        protected Vector3 m_moveDirection;
        protected float m_moveSpeed;
        protected bool m_isTargetPointPassed = false;

        public Vector3 Direction => m_moveDirection;

        public Vector3 DestionationPoint => m_targetPoint;

        public PathStepCalculator(ServerSettingsProvider serverSettings)
        {
            m_serverSettings = serverSettings;
        }

        public void SetTransform(Transform transform)
        {
            m_transform = transform;
        }

        public void SetDestionationPoint(Vector3 point, byte version)
        {
            m_isTargetPointPassed = false;
            m_targetPoint = point;
            m_targetVersion = version;
            m_moveDirection = m_targetPoint - m_transform.position;
            m_moveDirection.y = 0.0f;

            //Slightly increase the time of arrival of the object at the point so that the next packet has time to arrive
            m_moveSpeed = m_moveDirection.magnitude / (m_serverSettings.TickTime * 1.15f);
            m_moveDirection.Normalize();
        }

        public Step MakeStep()
        {
            Step step = new Step();
            step.delta = m_moveDirection * m_moveSpeed * Time.deltaTime;
            step.delta.y = GetGravity() * Time.deltaTime;

            if(m_isTargetPointPassed == false)
            {
                step.isTargetPointPassed = IsTargetPointPassed();
                OnDestinationReached(m_targetPoint, m_targetVersion);
            }

            return step;
        }

        public bool IsTargetPointPassed()
        {
            return m_isTargetPointPassed = Vector3.Dot(m_moveDirection, (m_targetPoint - m_transform.transform.position).GetClearY().normalized) < 0;
        }

        public virtual float GetGravity()
        {
            return -1f;
        }

        protected virtual void OnDestinationReached(Vector3 point, byte version)
        {

        }
    }
}
