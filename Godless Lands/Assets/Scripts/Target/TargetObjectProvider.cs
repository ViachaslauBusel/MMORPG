using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Target
{
    public class TargetObjectProvider
    {
        private TargetObjectRegistry _targetObjectRegistry;
        private TargetInformationService _targetInformationService;
        private ITargetObject _targetObject;
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
            _targetObjectRegistry.TryGetTargetObject(objectID, out _targetObject);

             OnTargetObjectChanged?.Invoke(_targetObject);
        }

        private void OnTargetObjectUnregistered(ITargetObject @object)
        {
            if(@object.ID == _targetObjectID)
            {
                _targetObject = null;
                OnTargetObjectChanged?.Invoke(null);
            }
        }

        private void OnTargetObjectRegistered(ITargetObject @object)
        {
            if(@object.ID == _targetObjectID && _targetObject == null)
            {
                _targetObject = @object;
                OnTargetObjectChanged?.Invoke(_targetObject);
            }
        }
    }
}
