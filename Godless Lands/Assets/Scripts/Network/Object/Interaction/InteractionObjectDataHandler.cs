using Network.Core;
using Network.Replication;
using Protocol.Data.Replicated;
using Protocol.MSG.Game.ObjectInteraction;
using Zenject;

namespace Network.Object.Interaction
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
