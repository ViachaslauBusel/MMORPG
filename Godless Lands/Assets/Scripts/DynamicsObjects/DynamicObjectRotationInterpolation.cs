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
    public class DynamicObjectRotationInterpolation : MonoBehaviour
    {
        private TransfromDataHandler m_transformData;
        private ISkinObject m_dynamicObjectHolder;
        private CharacterController m_characterController;
        private ServerSettingsProvider m_serverSettings;
        private Quaternion m_startRotation;
        private Quaternion m_targetRotation;
        private float m_rotationSpeed;
        private float m_rotationT;
        

        [Inject]
        private void Construct(ServerSettingsProvider serverSettings)
        {
            m_serverSettings = serverSettings;
        }

        private void Awake()
        {
            m_transformData = GetComponentInParent<TransfromDataHandler>();
            m_transformData.OnUpdateData += UpdateTrnsformData;

            m_dynamicObjectHolder = GetComponentInParent<ISkinObject>();
            m_dynamicObjectHolder.updateSkinObject += AssignSkinObject;
            AssignSkinObject(m_dynamicObjectHolder.SkinObject);
            enabled = false;
        }

        private void AssignSkinObject(GameObject dynamicObjectView)
        {
            m_characterController = dynamicObjectView.GetComponent<CharacterController>();
        }

        private void UpdateTrnsformData()
        {
            m_startRotation = m_characterController.transform.rotation;
            m_targetRotation = Quaternion.Euler(0, m_transformData.Rotation, 0);
            m_rotationSpeed = 1.0f / (m_serverSettings.TickTime * 1.1f);
            m_rotationT = 0.0f;

            enabled = true;
        }

        private void Update()
        {
            if (m_rotationT < 1.0f)
            {
                m_rotationT = Mathf.Clamp01(m_rotationT + m_rotationSpeed * Time.deltaTime);
                m_characterController.transform.rotation = Quaternion.Slerp(m_startRotation, m_targetRotation, m_rotationT);
            }
            else enabled = false;
        }

        private void OnDestroy()
        {
            m_transformData.OnUpdateData -= UpdateTrnsformData;
            m_dynamicObjectHolder.updateSkinObject -= AssignSkinObject;
        }
    }
}
