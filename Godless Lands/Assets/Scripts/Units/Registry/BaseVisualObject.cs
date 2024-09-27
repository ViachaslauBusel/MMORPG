using Network.Object.Visualization;
using Network.Replication;
using Protocol.Data.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units.Registry;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Units.Registry
{
    public abstract class BaseVisualObject : MonoBehaviour, IVisualObjectScript, IUnitVisualObject
    {
        private int _networkId;
        private UnitVisualObjectRegistry _unitObjectRegistry;

        public int NewtorkId => _networkId;
        public Transform Transform => transform;
        public abstract int UnitId { get; }
        public abstract UnitType UnitType { get; }
        public abstract string Nickname { get; }

        [Inject]
        private void Construct(UnitVisualObjectRegistry targetObjectRegistry)
        {
            _unitObjectRegistry = targetObjectRegistry;
        }

        public virtual void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            _networkId = networkObjectOwner.GetComponent<NetworkComponentsProvider>().NetworkGameObjectID;
            _unitObjectRegistry.Register(this);
            //Debug.Log($"TargetObject.AttachToNetworkObject: {_id}");
        }

        public virtual void DetachFromNetworkObject()
        {
            //Debug.Log($"TargetObject.DetachFromNetworkObject: {_id}");
            _unitObjectRegistry.Unregister(this);
            _networkId = 0;
        }
    }
}
