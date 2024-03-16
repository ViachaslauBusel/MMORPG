using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectInteraction
{
    /// <summary>
    /// Registry for all interactable objects in the scene.
    /// </summary>
    internal class InteractableObjectsRegistry
    {
        public struct InteractableObject
        {
            public int NetworkGameObjectID;
            public GameObject GameObject;
        }

        private Dictionary<int, GameObject> _interactableObjects = new Dictionary<int, GameObject>(); 

        
        internal void RegisterInteractableObject(int networkGameObjectID, GameObject gameObject)
        {
            if(_interactableObjects.ContainsKey(networkGameObjectID))
            {
                Debug.LogError($"Interactable object with id {networkGameObjectID} already exists in the registry");
                _interactableObjects.Remove(networkGameObjectID);
            }
            _interactableObjects.Add(networkGameObjectID, gameObject);
        }

        internal void UnregisterInteractableObject(int networkGameObjectID)
        {
            if (_interactableObjects.ContainsKey(networkGameObjectID) == false)
            {
                Debug.LogError($"Interactable object with id {networkGameObjectID} does not exist in the registry");
                return;
            }
            _interactableObjects.Remove(networkGameObjectID);
        }

        internal InteractableObject GetClosestInteractableObject(Vector3 position, float maxDistance = float.MaxValue)
        {
            InteractableObject closestObject = new InteractableObject();
            float closestDistance = maxDistance;

            foreach (var interactableObject in _interactableObjects)
            {
                float distance = Vector3.Distance(position, interactableObject.Value.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject.NetworkGameObjectID = interactableObject.Key;
                    closestObject.GameObject = interactableObject.Value;
                }
            }

            return closestObject;
        }

    }
}
