using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.Data.Replicated.Transform;
using System;
using UnityEngine;

namespace Network.Object.Dynamic.Transformations
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
