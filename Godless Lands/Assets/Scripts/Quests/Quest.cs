using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests
{
    public class Quest
    {
        private readonly int _questID;
        private readonly QuestData _questData;
        private int _currentStageID;

        public int ID => _questID;
        public int CurrentStageID => _currentStageID;
        public QuestData Data => _questData;

        public bool IsCompleted
        {
            get
            {
                QuestStageNode stageNode = GetCurrentStage();
                return stageNode == null || stageNode.NextNode == null;
            }
           
        }

        public Quest(int questID, QuestData questData, int currentStageID) 
        {
            _questID = questID;
            _questData = questData;
            _currentStageID = currentStageID;
        }

        internal void UpdateStageId(int stageID)
        {
            _currentStageID = stageID;
        }

        internal QuestStageNode GetCurrentStage()
        {
           return (QuestStageNode)_questData.Nodes.FirstOrDefault(x => x.ID == _currentStageID);
        }
    }
}
