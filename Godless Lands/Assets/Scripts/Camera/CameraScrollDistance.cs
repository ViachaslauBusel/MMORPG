using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MCamera
{
    internal class CameraScrollDistance : MonoBehaviour
    {
        [SerializeField]
        private Transform m_cameraTransform;
        [SerializeField]
        private Vector2 m_camDistance = new Vector2(-2, -5);
        [SerializeField]
        private float m_scrollSpeed = 20f;
        [SerializeField]
        private float m_smoothTime = 0.1f;
        private float m_velocity;
        private float m_distance;


        private void Start()
        {
            m_distance = m_cameraTransform.localPosition.z;
        }

        private void LateUpdate()
        {
            float mouseScrollDelata = Input.mouseScrollDelta.y;
            m_distance = Mathf.Clamp(m_distance + (mouseScrollDelata * m_scrollSpeed * Time.deltaTime), m_camDistance.y, m_camDistance.x);

            Vector3 camPosition = m_cameraTransform.localPosition;
            camPosition.z = Mathf.SmoothDamp(camPosition.z, m_distance, ref m_velocity, m_smoothTime);
            m_cameraTransform.localPosition = camPosition;
        }
    }
}
