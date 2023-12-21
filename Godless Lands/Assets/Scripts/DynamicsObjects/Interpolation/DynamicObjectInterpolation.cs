using DynamicsObjects.Interpolation;
using DynamicsObjects.TransformHandlers;
using Helpers;
using Protocol.Data.Replicated.Transform;
using Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace DynamicsObjects
{
    public class DynamicObjectInterpolation : MonoBehaviour
    {
        private TransfromDataHandler m_transformData;
        private TransformEventsHandler m_transformEvents;
        private ISkinObject m_dynamicObjectHolder;
        private CharacterController m_characterController;
        private ServerSettingsProvider m_serverSettings;
        private Animator m_animator;
        private PathStepJumpCalculator m_pathStepCalculator;
       // private Vector3 m_lastMoveDiraction;

    

       // private float m_gravity = 0f;
        private Action m_updatePosition = null;
        public Vector3 TEST_STEP;

        [Inject]
        private void Construct(ServerSettingsProvider serverSettings)
        {
            m_serverSettings = serverSettings;
        }

        private void Awake()
        {
            m_pathStepCalculator = new PathStepJumpCalculator(m_serverSettings);
            m_transformData = GetComponentInParent<TransfromDataHandler>();
            m_transformData.OnUpdateData += UpdateTrnsformData;
            m_transformEvents = GetComponentInParent<TransformEventsHandler>();
            m_transformEvents.OnServerReceivedEvent += ReceiveEvent;

            m_dynamicObjectHolder = GetComponentInParent<ISkinObject>();
            m_dynamicObjectHolder.updateSkinObject += AssignComponents;
            AssignComponents(m_dynamicObjectHolder.SkinObject);
        }

        private void ReceiveEvent(TransformEvent @event)
        {
            Debug.Log($"Receive event Jump");
            m_pathStepCalculator.PushEvent(@event);
        }

        private void AssignComponents(GameObject dynamicObjectView)
        {
            m_characterController = dynamicObjectView.GetComponent<CharacterController>();
            m_animator = dynamicObjectView.GetComponent<Animator>();
            m_pathStepCalculator.SetTransform(m_characterController.transform);
            m_pathStepCalculator.SetCharacterController(m_characterController);
            m_pathStepCalculator.SetAnimator(m_animator);
        }

        private void UpdateTrnsformData()
        {

            m_pathStepCalculator.SetDestionationPoint(m_transformData.Position, m_transformData.Version);

            //If object has stopped
            if (!m_transformData.InMove
            //And destination point behind -> stop the object immediately
            && m_pathStepCalculator.IsTargetPointPassed())
            {
                m_updatePosition = UpdatePositionInWait;
            }
            else if(!m_transformData.InMove) 
            {
                m_updatePosition = UpdatePositionInStop;
            }
            else if(m_transformData.InMove) 
            {
                m_updatePosition = UpdatePositionInMove;
            }

            Vector3 p_0 = m_characterController.transform.position;
            Vector3 p_1 = m_pathStepCalculator.DestionationPoint;
            p_0.y = p_1.y = 0.0f;
            float distance = Vector3.Distance(p_1, p_0);
            if (distance > 4.0f )
            {
                Debug.LogError($"Position out of sync, position correction");
                m_updatePosition = UpdatePositionInWait;
                TeleportTo(m_pathStepCalculator.DestionationPoint);
            }
        }

        public void TeleportTo(Vector3 position)
        {
            m_characterController.enabled = false;
            m_characterController.transform.position = (position);
            m_characterController.enabled = true;
        }

        private void UpdatePositionInMove()
        {
            Step step = m_pathStepCalculator.MakeStep();
            m_characterController.Move(step.delta);
        }
        private void UpdatePositionInStop()
        {
            Step step = m_pathStepCalculator.MakeStep();
            m_characterController.Move(step.delta);

            if(step.isTargetPointPassed)
            {
                m_updatePosition = UpdatePositionInWait;
            }
        }
        private void UpdatePositionInWait()
        {
            Vector3 step = Vector3.zero;
            step.y = m_pathStepCalculator.GetGravity() * Time.deltaTime;
            TEST_STEP = step;
            m_characterController.Move(step);
        }

        private void Update()
        {
            m_updatePosition?.Invoke();
        }

        private void OnDestroy()
        {
            m_transformData.OnUpdateData -= UpdateTrnsformData;
            m_transformEvents.OnServerReceivedEvent -= ReceiveEvent;
        }
    }
}
