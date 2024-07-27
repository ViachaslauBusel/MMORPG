using Helpers;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Transform;
using System;
using UnityEngine;

namespace Network.Object.Dynamic.Transformations
{
    public class TransfromDataHandler : MonoBehaviour, INetworkDataHandler
    {
        private TransformData _transformData;

        public event Action OnUpdateData;

        public Vector3 Position => _transformData.Position.ToUnity();
        public float Rotation => _transformData.Rotation;
        public bool InMove => _transformData.InMove;
        public float Velocity => _transformData.Velocity;
        public byte Version => _transformData.Version;

        public void UpdateData(IReplicationData updatedData)
        {
            _transformData = (TransformData)updatedData;
            OnUpdateData?.Invoke();
        }
    }
}
