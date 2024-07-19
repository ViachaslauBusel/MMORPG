using Network.Replication;
using Protocol.Data.Replicated;
using System;
using UnityEngine;

namespace Nickname
{
    public class UnitNameHandler : MonoBehaviour, INetworkDataHandler
    {
        public string UnitName { get; private set; }

        public event Action<string> OnNameChanged;

        public void UpdateData(IReplicationData updatedData)
        {
            UnitName = ((UnitName)updatedData).Name;
            OnNameChanged?.Invoke(UnitName);
        }
    }
}
