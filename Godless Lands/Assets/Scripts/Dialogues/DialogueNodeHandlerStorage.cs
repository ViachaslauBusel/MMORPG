using Dialogues.Data;
using Dialogues.Data.Nodes;
using Dialogues.NodeHandlers;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Dialogues
{
    internal class DialogueNodeHandlerStorage
    {
        private Dictionary<Type, IDialogExecutionNodeHandler> _executionHandlers = new Dictionary<Type, IDialogExecutionNodeHandler>();
        private Dictionary<Type, IDialogConditionNodeHandler> _conditionHandlers = new Dictionary<Type, IDialogConditionNodeHandler>();

        public DialogueNodeHandlerStorage(DiContainer diContainer)
        {
            _executionHandlers.Add(typeof(QuestLevelUpNode), diContainer.Resolve<QuestLevelUpNodeHandler>());
            _conditionHandlers.Add(typeof(QuestLevelCheckNode), diContainer.Resolve<QuestLevelCheckNodeHandler>());
        }

        internal bool TryGetExecutionHandler(IExecutionNode executionNode, out IDialogExecutionNodeHandler handler)
        {
            return _executionHandlers.TryGetValue(executionNode.GetType(), out handler);
        }

        internal bool TryGeConditionHandler(IConditionNode conditionNode, out IDialogConditionNodeHandler handler)
        {
            return _conditionHandlers.TryGetValue(conditionNode.GetType(), out handler);
        }
    }
}
