using Helpers;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Transform;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

