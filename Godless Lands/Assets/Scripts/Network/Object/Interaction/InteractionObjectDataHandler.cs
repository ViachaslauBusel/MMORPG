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
        private InteractionController _interactionController;

        [Inject]
        private void Construct(InteractionController interactionController)
        {
            _interactionController = interactionController;
        }

        public override void HandleInteraction()
        {
          _interactionController.StartInteraction(NetworkGameObjectID);
        }

        public void UpdateData(IReplicationData updatedData)
        {
        }
    }
}
