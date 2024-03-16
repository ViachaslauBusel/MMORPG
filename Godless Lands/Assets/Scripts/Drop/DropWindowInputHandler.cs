using Protocol.MSG.Game.Drop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Drop
{
    public class DropWindowInputHandler : MonoBehaviour
    {
        private NetworkManager _networkManager;

        [Inject]
        public void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void TakeAllDrop()
        {
            MSG_DROP_COMMAND_CS request = new MSG_DROP_COMMAND_CS();
            request.CommandType = DropCommandType.TakeAllItems;
            _networkManager.Client.Send(request);
            Debug.Log("Take all drop");
        }

        public void EndInteraction()
        {
            MSG_DROP_COMMAND_CS request = new MSG_DROP_COMMAND_CS();
            request.CommandType = DropCommandType.EndInteraction;
            _networkManager.Client.Send(request);
        }
    }
}
