using NetworkObjectVisualization;
using Services.Replication;
using UnityEngine;
using Zenject;

namespace Target
{
    internal class TargetObject : MonoBehaviour, ITargetObject, IVisualObjectScript
    {
        private int _id;
        private TargetObjectRegistry _targetObjectRegistry;

        public int ID => _id;
        public Transform Transform => transform;

        [Inject]
        private void Construct(TargetObjectRegistry targetObjectRegistry)
        {
            _targetObjectRegistry = targetObjectRegistry;
        }

        public void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            _id = networkObjectOwner.GetComponent<NetworkComponentsProvider>().NetworkGameObjectID;
            _targetObjectRegistry.Register(this);
            //Debug.Log($"TargetObject.AttachToNetworkObject: {_id}");
        }

        public void DetachFromNetworkObject()
        {
            //Debug.Log($"TargetObject.DetachFromNetworkObject: {_id}");
            _targetObjectRegistry.Unregister(this);
            _id = 0;
        }
    }
}
