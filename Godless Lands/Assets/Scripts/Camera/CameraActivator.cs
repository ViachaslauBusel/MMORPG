using Network.Object.Visualization;
using UnityEngine;
using Zenject;

namespace MCamera
{
    public class CameraActivator : MonoBehaviour
    {
        private IVisualRepresentation m_skinObjectHolder;
        private CameraController m_cameraController;

        [Inject]
        private void Construct(CameraController controller)
        {
            m_cameraController = controller;
        }

        private void Awake()
        {
            m_skinObjectHolder = GetComponentInParent<IVisualRepresentation>();
        }

        private void Start()
        {
            SetCameraTrackingPoint(m_skinObjectHolder.VisualObject);

            m_skinObjectHolder.OnVisualObjectUpdated += SetCameraTrackingPoint;
        }

        private void SetCameraTrackingPoint(GameObject trackingPoint)
        {
            CharacterController characterController = trackingPoint.GetComponent<CharacterController>();
            m_cameraController.SetTrackingCharacter(characterController);
        }

        private void OnDestroy()
        {
            m_skinObjectHolder.OnVisualObjectUpdated -= SetCameraTrackingPoint;
            m_cameraController.SetTrackingCharacter(null);
        }
    }
}
