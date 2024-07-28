using System;
using System.Collections.Generic;
using UnityEngine;

namespace Target
{
    public class TargetObjectRegistry
    {
        private Dictionary<int, ITargetObject> _targetObjects = new Dictionary<int, ITargetObject>();

        public event Action<ITargetObject> OnTargetObjectRegistered;
        public event Action<ITargetObject> OnTargetObjectUnregistered;

        public void Register(ITargetObject targetObject)
        {
            if(_targetObjects.ContainsKey(targetObject.ID))
            {
                Debug.LogError($"TargetObjectRegistry.Register: target with id {targetObject.ID} already exists");
                return;
            }
            _targetObjects.Add(targetObject.ID, targetObject);
            OnTargetObjectRegistered?.Invoke(targetObject);
        }

        public void Unregister(ITargetObject targetObject)
        {
            if(!_targetObjects.ContainsKey(targetObject.ID))
            {
                Debug.LogError($"TargetObjectRegistry.Unregister: target with id {targetObject.ID} does not exist");
                return;
            }
            _targetObjects.Remove(targetObject.ID);
            OnTargetObjectUnregistered?.Invoke(targetObject);
        }

        public ITargetObject Get(int id)
        {
            return _targetObjects[id];
        }

        internal void TryGetTargetObject(int objectID, out ITargetObject targetObject)
        {
           _targetObjects.TryGetValue(objectID, out targetObject);
        }
    }
}
