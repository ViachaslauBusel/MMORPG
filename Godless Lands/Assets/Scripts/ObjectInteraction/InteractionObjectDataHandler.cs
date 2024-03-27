using NetworkObjectVisualization;
using Protocol.Data.Replicated;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ObjectInteraction
{
    /// <summary>
    /// Registers visual object as interactable object
    /// </summary>
    public class InteractionObjectDataHandler : MonoBehaviour, INetworkDataHandler
    {
        private NetworkComponentsProvider _networkComponentsProvider;
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private IVisualRepresentation _visualRepresentation;
        private GameObject _visualObject;

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

        private void OnVisualObjectUpdated(GameObject @object)
        {
            RegisterInteractableObject(@object);
        }

        public void UpdateData(IReplicationData updatedData)
        {
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

            _interactableObjectsRegistry.RegisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID, visualObject);
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
