using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests
{
    public struct Quest
    {
        private int m_questID;
        private int m_currentStageID;
        private QuestData m_questData;
        private IEnumerable<int> m_stagesLog;

        public Quest(int questID, QuestData questData, int currentStageID, List<int> stagesLog) 
        {
            m_questID = questID;
            m_questData = questData;
            m_currentStageID = currentStageID;
            m_stagesLog = stagesLog;
        }

        public int ID => m_questID;
        public int CurrentStageID => m_currentStageID;
        public QuestData Data => m_questData;
        public IEnumerable<int> StagesLog => m_stagesLog;
    }
}
