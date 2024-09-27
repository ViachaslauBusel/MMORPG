using System;
using System.Collections.Generic;
using Target;
using UnityEngine;

namespace Units.Registry
{
    public class UnitVisualObjectRegistry
    {
        private Dictionary<int, IUnitVisualObject> _unitObjects = new Dictionary<int, IUnitVisualObject>();

        public event Action<IUnitVisualObject> OnUnitVisualObjectRegistered;
        public event Action<IUnitVisualObject> OnUnitVisualObjectUnregistered;

        public void Register(IUnitVisualObject targetObject)
        {
            if(_unitObjects.ContainsKey(targetObject.NewtorkId))
            {
                Debug.LogError($"TargetObjectRegistry.Register: target with id {targetObject.NewtorkId} already exists");
                return;
            }
            _unitObjects.Add(targetObject.NewtorkId, targetObject);
            OnUnitVisualObjectRegistered?.Invoke(targetObject);
        }

        public void Unregister(IUnitVisualObject targetObject)
        {
            if(!_unitObjects.ContainsKey(targetObject.NewtorkId))
            {
                Debug.LogError($"TargetObjectRegistry.Unregister: target with id {targetObject.NewtorkId} does not exist");
                return;
            }
            _unitObjects.Remove(targetObject.NewtorkId);
            OnUnitVisualObjectUnregistered?.Invoke(targetObject);
        }

        public IUnitVisualObject GetUnitVisualObjectByNetworkId(int id)
        {
            if (!_unitObjects.ContainsKey(id))
            {
                return null;
            }
            return _unitObjects[id];
        }

        internal void TryGetTargetObject(int objectID, out IUnitVisualObject targetObject)
        {
           _unitObjects.TryGetValue(objectID, out targetObject);
        }
    }
}
