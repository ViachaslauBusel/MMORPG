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
            // Check if this quest exists in the journal
            bool isExist = _questsModel.TryGetQuestById(questLevelCheck.QuestId, out Quest quest);

            // If the quest does not exist in the journal, then the ID of its current stage is 0
            int questStageID = isExist ? quest.CurrentStageID : 0;

            // Compare the quest stage ID with the required one
            if(questStageID == questLevelCheck.RequiredStageId) return questLevelCheck.SuccesNode;
            
            return questLevelCheck.FailNode;
        }
    }
}
