using Cysharp.Threading.Tasks;
using Dialogues.Data;
using NodeEditor.Data;

namespace Dialogues
{
    public interface IDialogExecutionNodeHandler
    {
        UniTask<Node> Execute(IExecutionNode executionNode, Network.Object.Interaction.IInteractableObject _npc);
    }
}
