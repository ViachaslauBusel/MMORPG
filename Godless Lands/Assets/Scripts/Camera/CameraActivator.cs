using Player;
using Skins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MCamera
{
    public class CameraActivator : MonoBehaviour
    {
        private ISkinObject m_skinObjectHolder;
        private CameraController m_cameraController;

        [Inject]
        private void Construct(CameraController controller)
        {
            m_cameraController = controller;
        }

        private void Awake()
        {
            m_skinObjectHolder = GetComponentInParent<ISkinObject>();
        }

        private void Start()
        {
            SetCameraTrackingPoint(m_skinObjectHolder.SkinObject);

            m_skinObjectHolder.updateSkinObject += SetCameraTrackingPoint;
        }

        private void SetCameraTrackingPoint(GameObject trackingPoint)
        {
            CharacterController characterController = trackingPoint.GetComponent<CharacterController>();
            m_cameraController.SetTrackingCharacter(characterController);
        }

        private void OnDestroy()
        {
            m_skinObjectHolder.updateSkinObject -= SetCameraTrackingPoint;
            m_cameraController.SetTrackingCharacter(null);
        }
    }
}
