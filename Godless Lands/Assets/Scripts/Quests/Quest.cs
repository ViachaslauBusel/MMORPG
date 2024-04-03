using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests
{
    public struct Quest
    {
        private int _questID;
        private int _currentStageID;
        private QuestData _questData;
        private IEnumerable<int> _stagesLog;

        public Quest(int questID, QuestData questData, int currentStageID, List<int> stagesLog) 
        {
            _questID = questID;
            _questData = questData;
            _currentStageID = currentStageID;
            _stagesLog = stagesLog;
        }

        public int ID => _questID;
        public int CurrentStageID => _currentStageID;
        public QuestData Data => _questData;
        public IEnumerable<int> StagesLog => _stagesLog;
    }
}
