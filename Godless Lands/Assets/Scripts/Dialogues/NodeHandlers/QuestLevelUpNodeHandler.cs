using Cysharp.Threading.Tasks;
using Dialogues.Data;
using Dialogues.Data.Nodes;
using NodeEditor.Data;
using Quests;

namespace Dialogues.NodeHandlers
{
    internal class QuestLevelUpNodeHandler : IDialogExecutionNodeHandler
    {
        private QuestsController _questsController;
        private Node _next;

        public Node Next => _next;

        public QuestLevelUpNodeHandler(QuestsController questsController)
        {
            _questsController = questsController;
        }

        public async UniTask<Node> Execute(IExecutionNode executionNode)
        {
            QuestLevelUpNode node = executionNode as QuestLevelUpNode;
            if (node == null) return null;

            var task = _questsController.IncreaseQuestStage(node.QuestId);
            await UniTask.WaitUntil(() => task.IsCompleted);
            bool questUpResult = task.Result;
            return questUpResult ? node.SuccesNode : node.FailNode;
        }
    }
}
