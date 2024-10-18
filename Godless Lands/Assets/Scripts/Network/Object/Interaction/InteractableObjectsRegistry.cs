using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Network.Object.Interaction
{
    /// <summary>
    /// Registry for all interactable objects in the scene.
    /// </summary>
    internal class InteractableObjectsRegistry
    {
        private Dictionary<int, IInteractableObject> _interactableObjects = new Dictionary<int, IInteractableObject>(); 

        
        internal void RegisterInteractableObject(int networkGameObjectID, IInteractableObject interactionObject)
        {
            if(_interactableObjects.ContainsKey(networkGameObjectID))
            {
                Debug.LogError($"Interactable object with id {networkGameObjectID} already exists in the registry");
                _interactableObjects.Remove(networkGameObjectID);
            }
            _interactableObjects.Add(networkGameObjectID, interactionObject);
        }

        internal void UnregisterInteractableObject(int networkGameObjectID)
        {
            if (_interactableObjects.ContainsKey(networkGameObjectID) == false)
            {
               // Debug.LogError($"Interactable object with id {networkGameObjectID} does not exist in the registry");
                return;
            }
            _interactableObjects.Remove(networkGameObjectID);
        }

        internal IInteractableObject GetClosestInteractableObject(Vector3 position, float maxDistance = float.MaxValue)
        {
            IInteractableObject closestObject = null;
            float closestDistance = maxDistance;

            foreach (var interactableObject in _interactableObjects)
            {
                float distance = Vector3.Distance(position, interactableObject.Value.Position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = interactableObject.Value;
                }
            }

            return closestObject;
        }

    }
}
