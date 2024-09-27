using System;
using Units.Registry;

namespace Target
{
    public class TargetObjectProvider
    {
        private UnitVisualObjectRegistry _unitObjectRegistry;
        private TargetListener _targetInformationService;
        private int _targetObjectID;

        public int TargetObjectID => _targetObjectID;

        public event Action<IUnitVisualObject> OnTargetObjectChanged;

        public TargetObjectProvider(UnitVisualObjectRegistry targetObjectRegistry, TargetListener targetInformationService)
        {
            _unitObjectRegistry = targetObjectRegistry;
            _targetInformationService = targetInformationService;

            _unitObjectRegistry.OnUnitVisualObjectRegistered += OnTargetObjectRegistered;
            _unitObjectRegistry.OnUnitVisualObjectUnregistered += OnTargetObjectUnregistered;

            _targetInformationService.OnTargetObjectChanged += TargetObjectChanged;
        }

        private void TargetObjectChanged(int objectID, string nickname, float percentHp)
        {
            _targetObjectID = objectID;
            _unitObjectRegistry.TryGetTargetObject(objectID, out IUnitVisualObject targetObject);

             OnTargetObjectChanged?.Invoke(targetObject);
        }

        private void OnTargetObjectUnregistered(IUnitVisualObject @object)
        {
            //Debug.Log($"Unregistered target object: {@object.ID} curT:{_targetObjectID}");
            if(@object.NewtorkId == _targetObjectID)
            {
                _targetObjectID = 0;
                OnTargetObjectChanged?.Invoke(null);
            }
        }

        private void OnTargetObjectRegistered(IUnitVisualObject @object)
        {
            if(@object.NewtorkId == _targetObjectID)
            {
                OnTargetObjectChanged?.Invoke(@object);
            }
        }
    }
}
