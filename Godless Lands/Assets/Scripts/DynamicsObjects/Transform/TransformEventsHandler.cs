using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Transform;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DynamicsObjects.TransformHandlers
{
    public class TransformEventsHandler : MonoBehaviour, INetworkDataHandler
    {
        public event Action<TransformEvent> OnServerReceivedEvent;
        public void UpdateData(IReplicationData updatedData)
        {
            TransformEvents events = (TransformEvents)updatedData;
            foreach (var e in events.Events)
            {
               OnServerReceivedEvent?.Invoke(e);
            }
        }
    }
}
