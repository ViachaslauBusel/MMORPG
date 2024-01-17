using Protocol.Data.Replicated;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
