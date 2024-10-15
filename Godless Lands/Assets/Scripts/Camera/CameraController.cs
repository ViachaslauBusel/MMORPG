using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController m_trackingCharacter;
        [SerializeField]
        private float speed_rotation = 100.0f;
        [SerializeField]
        private float m_height = 2.5f;
        private Camera m_camera;
        private Vector3 _rotation;

        public Camera Camera { get => m_camera; }

        private void Awake()
        {
            m_camera = GetComponentInChildren<Camera>();
        }
        private void Start()
        {
            _rotation = transform.rotation.eulerAngles;
            //If the tracking point is not set, disable the script
            if (m_trackingCharacter == null)
            { enabled = false; }
        }

        public void SetTrackingCharacter(CharacterController characterController)
        {
            m_trackingCharacter = characterController;

            enabled = m_trackingCharacter != null;
        }

        private void LateUpdate()
        {
            if (Input.GetButton("MouseRight"))
            {
                if (Input.GetButton("MouseLeft"))
                {
                    _rotation.x -= (Input.GetAxis("Mouse Y") * speed_rotation) * Time.deltaTime;
                    _rotation.x = Mathf.Clamp(_rotation.x, -50.0f, 65.0f);

                    _rotation.y += (Input.GetAxis("Mouse X") * speed_rotation) * Time.deltaTime;
                    //print(rotation);

                    transform.localRotation = Quaternion.Euler(_rotation);
                }
                else
                {
                    /* if (Input.GetButton("Jump"))
                     {
                         transform.localRotation = Quaternion.Euler(Vector3.zero);
                     }*/
                    Vector3 targetRotation = m_trackingCharacter.transform.rotation.eulerAngles;
                    targetRotation.y = transform.rotation.eulerAngles.y + (Input.GetAxis("Mouse X") * speed_rotation) * Time.deltaTime;

                    // Rotate the character around horizaontal axis using mouse axis X
                    if (m_trackingCharacter != null && m_trackingCharacter.enabled)
                    {
                        m_trackingCharacter.transform.rotation = Quaternion.Euler(targetRotation);
                    }

                    // Rotate the camera around vertical axis using mouse axis Y
                    _rotation.y = targetRotation.y;
                    _rotation.x -= (Input.GetAxis("Mouse Y") * speed_rotation) * Time.deltaTime;
                    _rotation.x = Mathf.Clamp(_rotation.x, -50.0f, 65.0f);

                    transform.rotation = Quaternion.Euler(_rotation);
                }
            }
            // Return camera to the back
            else if (m_trackingCharacter != null && m_trackingCharacter.enabled)
            {
                _rotation = transform.rotation.eulerAngles;
                _rotation.y = Mathf.MoveTowardsAngle(_rotation.y, m_trackingCharacter.transform.rotation.eulerAngles.y, speed_rotation * Time.deltaTime);
                transform.rotation = Quaternion.Euler(_rotation);
            }
          
            //float targetRotationY = m_trackingPoint.rotation.eulerAngles.y;
            //float deltaAngle = Mathf.DeltaAngle(rotation.y, targetRotationY); 
            //if (deltaAngle != 0.0f && !Input.GetButton("MouseLeft"))
            //{
            //    if (deltaAngle < 0.0f)
            //    {
            //        deltaAngle += speed_rotation * Time.deltaTime;
            //        if (deltaAngle > 0.0f) rotation.y = 0.0f;
            //    }
            //    else
            //    {
            //        deltaAngle -= speed_rotation * Time.deltaTime;
            //        if (deltaAngle < 0.0f) deltaAngle = 0.0f;
            //    }
            //    rotation.y = targetRotationY - deltaAngle;
            //    transform.rotation = Quaternion.Euler(rotation);
            //}

            transform.position = m_trackingCharacter.transform.position + new Vector3(0, m_height, 0);
        }
    }
}