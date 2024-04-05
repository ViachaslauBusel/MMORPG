using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.Journal
{
    internal class QuestJournalModel
    {
        private int _currentSelectedQuestId;

        public event Action<int> OnQuestSelected;

        public int CurrentSelectedQuestId => _currentSelectedQuestId;

        public void SetCurrentSelectedQuestId(int questId)
        {
            _currentSelectedQuestId = questId;
            OnQuestSelected?.Invoke(questId);
        }
    }
}
