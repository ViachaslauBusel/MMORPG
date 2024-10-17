using Dialogues.Data;
using Dialogues.Data.Nodes;
using NodeEditor.Data;
using Protocol.Data.Quests;
using Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Dialogues.NodeHandlers
{
    internal class QuestLevelCheckNodeHandler : IDialogConditionNodeHandler
    {
        private QuestsModel _questsModel;
        private Node _next;

        public Node Next => _next;

       public QuestLevelCheckNodeHandler(QuestsModel questsModel)
        {
            _questsModel = questsModel;
        }

        public Node CheckCondition(IConditionNode conditionNode)
        {
            QuestLevelCheckNode questLevelCheck = conditionNode as QuestLevelCheckNode;
            if (questLevelCheck == null) return null;

            // Get the current quest stage ID
            int questStageID = _questsModel.GetQuestCurrentStageID(questLevelCheck.QuestId);

            // Compare the quest stage ID with the required one
            if (questStageID == questLevelCheck.RequiredStageId) return questLevelCheck.SuccesNode;
            
            return questLevelCheck.FailNode;
        }
    }
}
