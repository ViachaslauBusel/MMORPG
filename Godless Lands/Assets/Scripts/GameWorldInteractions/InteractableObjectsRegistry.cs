using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameWorldInteractions
{
    internal class InteractableObjectsRegistry
    {
        private List<GameObject> _interactableObjects = new List<GameObject>(); 

        
        internal void RegisterInteractableObject(GameObject gameObject)
        {
           _interactableObjects.Add(gameObject);
        }

        internal void UnregisterInteractableObject(GameObject gameObject)
        {
           _interactableObjects.Remove(gameObject);
        }

        internal GameObject GetClosestInteractableObject(Vector3 position, float maxDistance = float.MaxValue)
        {
            GameObject closestObject = null;
            float closestDistance = maxDistance;

            foreach (var interactableObject in _interactableObjects)
            {
                float distance = Vector3.Distance(position, interactableObject.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = interactableObject;
                }
            }

            return closestObject;
        }

    }
}
