using Cysharp.Threading.Tasks;
using Dialogues.Data;
using Dialogues.Data.Nodes;
using Network.Object.Interaction;
using NodeEditor.Data;
using Quests;

namespace Dialogues.NodeHandlers
{
    public class StoreNodeHandler : IDialogExecutionNodeHandler
    {
        private DialogWindow _dialogWindow;
        private InteractionController _interactionController;
        private Node _next;

        public Node Next => _next;

        public StoreNodeHandler(InteractionController interactionController, DialogWindow dialogWindow)
        {
            _interactionController = interactionController;
            _dialogWindow = dialogWindow;
        }

        public UniTask<Node> Execute(IExecutionNode executionNode, IInteractableObject npc)
        {
            _dialogWindow.Close();
            _interactionController.StartInteraction(npc.NetworkGameObjectID);
            return default;
        }
    }
}
