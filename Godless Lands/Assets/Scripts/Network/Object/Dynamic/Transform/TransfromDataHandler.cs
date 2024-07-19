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
        private TransformData m_transformData;

        public event Action OnUpdateData;

        public Vector3 Position => m_transformData.Position.ToUnity();
        public float Rotation => m_transformData.Rotation;
        public bool InMove => m_transformData.InMove;
        public float Velocity => m_transformData.Velocity;
        public byte Version => m_transformData.Version;

        public void UpdateData(IReplicationData updatedData)
        {
            m_transformData = (TransformData)updatedData;
            OnUpdateData?.Invoke();
        }
    }
}
