using Network.Core;
using Protocol.MSG.Game.Drop;
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
        }

        public void TakeItem(int itemIndex)
        {
            MSG_DROP_COMMAND_CS request = new MSG_DROP_COMMAND_CS();
            request.CommandType = DropCommandType.TakeItem;
            request.DropIndex = itemIndex;
            _networkManager.Client.Send(request);
        }

        public void EndInteraction()
        {
            MSG_DROP_COMMAND_CS request = new MSG_DROP_COMMAND_CS();
            request.CommandType = DropCommandType.EndInteraction;
            _networkManager.Client.Send(request);
        }
    }
}
