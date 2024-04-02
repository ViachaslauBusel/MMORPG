using NetworkObjectVisualization;
using Services.Replication;
using UnityEngine;
using Zenject;

namespace ObjectInteraction
{
    public abstract class InterableObject : MonoBehaviour, IInteractableObject
    {
        private NetworkComponentsProvider _networkComponentsProvider;
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private IVisualRepresentation _visualRepresentation;
        private GameObject _visualObject;

        public int NetworkGameObjectID => _networkComponentsProvider.NetworkGameObjectID;
        public Vector3 Position => _visualObject != null ? _visualObject.transform.position : Vector3.zero;

        [Inject]
        private void Construct(InteractableObjectsRegistry interactableObjectsRegistry)
        {
            _interactableObjectsRegistry = interactableObjectsRegistry;
        }

        private void Start()
        {
            _networkComponentsProvider = GetComponent<NetworkComponentsProvider>();
            _visualRepresentation = GetComponent<IVisualRepresentation>();

            OnVisualObjectUpdated(_visualRepresentation.VisualObject);
            _visualRepresentation.OnVisualObjectUpdated += OnVisualObjectUpdated;
        }

        public abstract void HandleInteraction();

        private void OnVisualObjectUpdated(GameObject @object)
        {
            RegisterInteractableObject(@object);
        }

        /// <summary>
        /// Registers visual object as interactable object
        /// Also unregisters previous object if it was registered
        /// </summary>
        /// <param name="visualObject"></param>
        private void RegisterInteractableObject(GameObject visualObject)
        {
            // Make sure to unregister the previous object
            UnregisterInteractableObject();

            _visualObject = visualObject;
            if (_visualObject == null) return;

            _interactableObjectsRegistry.RegisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID, this);
        }

        private void UnregisterInteractableObject()
        {
            if (_visualObject == null) return;
            _interactableObjectsRegistry.UnregisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID);
            _visualObject = null;
        }

        private void OnDestroy()
        {
            UnregisterInteractableObject();
        }
    }
}
