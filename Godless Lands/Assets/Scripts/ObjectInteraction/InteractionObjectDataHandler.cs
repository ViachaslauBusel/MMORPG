using NetworkObjectVisualization;
using Protocol.Data.Replicated;
using Protocol.MSG.Game.ObjectInteraction;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ObjectInteraction
{
    /// <summary>
    /// Registers visual object as interactable object
    /// </summary>
    public class InteractionObjectDataHandler : InterableObject, INetworkDataHandler
    {
        private NetworkManager _networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public override void HandleInteraction()
        {
            MSG_OBJECT_INTERACTION_REQUEST_CS msg = new MSG_OBJECT_INTERACTION_REQUEST_CS();
            msg.ObjectId = NetworkGameObjectID;
            _networkManager.Client.Send(msg);
        }

        public void UpdateData(IReplicationData updatedData)
        {
        }
    }
}
