using GameWorldInteractions;
using NetworkObjectVisualization;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Drop;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Drop
{
    public class LootableUnitDataHandler : MonoBehaviour, INetworkDataHandler
    {
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private IVisualRepresentation _visualRepresentation;
        private GameObject _visualObject;

        [Inject]
        private void Construct(InteractableObjectsRegistry interactableObjectsRegistry)
        {
            _interactableObjectsRegistry = interactableObjectsRegistry;
        }

        private void Awake()
        {
            _visualRepresentation = GetComponent<IVisualRepresentation>();
        }

        private void Start()
        {
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

        private void RegisterInteractableObject(GameObject visualObject)
        {
            if(visualObject == _visualObject) return;

            if (_visualObject != null)
            {
                _interactableObjectsRegistry.UnregisterInteractableObject(_visualObject);
            }
            _visualObject = visualObject;

            if(_visualObject == null) return;

            _interactableObjectsRegistry.RegisterInteractableObject(visualObject);
        }

        private void UnregisterInteractableObject()
        {
            if (_visualObject == null) return;
            _interactableObjectsRegistry.UnregisterInteractableObject(_visualObject);
            _visualObject = null;
        }

        private void OnDestroy()
        {
           UnregisterInteractableObject();
        }
    }
}
