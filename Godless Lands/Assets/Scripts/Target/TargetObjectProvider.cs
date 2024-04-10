using System;

namespace Target
{
    public class TargetObjectProvider
    {
        private TargetObjectRegistry _targetObjectRegistry;
        private TargetInformationService _targetInformationService;
        private int _targetObjectID;

        public event Action<ITargetObject> OnTargetObjectChanged;

        public TargetObjectProvider(TargetObjectRegistry targetObjectRegistry, TargetInformationService targetInformationService)
        {
            _targetObjectRegistry = targetObjectRegistry;
            _targetInformationService = targetInformationService;

            _targetObjectRegistry.OnTargetObjectRegistered += OnTargetObjectRegistered;
            _targetObjectRegistry.OnTargetObjectUnregistered += OnTargetObjectUnregistered;

            _targetInformationService.OnTargetObjectChanged += TargetObjectChanged;
        }

        private void TargetObjectChanged(int objectID, string nickname, float percentHp)
        {
            _targetObjectID = objectID;
            _targetObjectRegistry.TryGetTargetObject(objectID, out ITargetObject targetObject);

             OnTargetObjectChanged?.Invoke(targetObject);
        }

        private void OnTargetObjectUnregistered(ITargetObject @object)
        {
            //Debug.Log($"Unregistered target object: {@object.ID} curT:{_targetObjectID}");
            if(@object.ID == _targetObjectID)
            {
                _targetObjectID = 0;
                OnTargetObjectChanged?.Invoke(null);
            }
        }

        private void OnTargetObjectRegistered(ITargetObject @object)
        {
            if(@object.ID == _targetObjectID)
            {
                OnTargetObjectChanged?.Invoke(@object);
            }
        }
    }
}
