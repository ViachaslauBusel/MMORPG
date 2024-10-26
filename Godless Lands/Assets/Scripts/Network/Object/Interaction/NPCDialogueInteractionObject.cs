using Network.Object.Visualization;
using Network.Replication;
using UnityEngine;
using Zenject;

namespace Network.Object.Interaction
{
    public class NPCDialogueInteractionObject : MonoBehaviour, IInteractableObject, IVisualObjectScript
    {
        private DialogData _dialogData;
        private NetworkComponentsProvider _networkComponentsProvider;
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private DialogWindow _dialogWindow;

        public int NetworkGameObjectID => _networkComponentsProvider.NetworkGameObjectID;

        public Vector3 Position => transform.position;

        [Inject]
        private void Construct(InteractableObjectsRegistry interactableObjectsRegistry, DialogWindow dialogWindow)
        {
            _interactableObjectsRegistry = interactableObjectsRegistry;
            _dialogWindow = dialogWindow;
        }

        public void Init(DialogData dialogData)
        {
            _dialogData = dialogData;
        }

        public void AttachToNetworkObject(GameObject networkObjectOwner)
        {
            _networkComponentsProvider = GetComponentInParent<NetworkComponentsProvider>();
            _interactableObjectsRegistry.RegisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID, this);
        }

        public void HandleInteraction()
        {
            _dialogWindow.Open(this);
            _dialogWindow.OpenDialog(_dialogData.StartNode.Next);
        }

        public void DetachFromNetworkObject()
        {
            _interactableObjectsRegistry.UnregisterInteractableObject(_networkComponentsProvider.NetworkGameObjectID);
        }
    }
}
