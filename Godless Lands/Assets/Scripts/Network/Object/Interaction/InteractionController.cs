using Network.Core;
using Protocol.MSG.Game.ObjectInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Network.Object.Interaction
{
    public class InteractionController
    {
        private NetworkManager _networkManager;

        public InteractionController(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }


        public void StartInteraction(int networkGameObjectID)
        {
            MSG_OBJECT_INTERACTION_REQUEST_CS msg = new MSG_OBJECT_INTERACTION_REQUEST_CS();
            msg.ObjectId = networkGameObjectID;
            _networkManager.Client.Send(msg);
        }
    }
}
