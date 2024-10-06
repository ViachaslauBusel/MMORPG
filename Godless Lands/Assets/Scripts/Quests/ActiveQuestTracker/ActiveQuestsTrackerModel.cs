using System;
using System.Collections.Generic;

namespace Quests.ActiveQuestTracker
{
    public class ActiveQuestsTrackerModel
    {
        private List<int> _activeQuests = new List<int>();


        public Action<int> OnQuestStartTracking;
        public Action OnQuestListUpdated;

        public IReadOnlyCollection<int> Quests => _activeQuests;

        internal void AddQuest(int questId)
        {
            _activeQuests.Add(questId);
            OnQuestStartTracking?.Invoke(questId);
            OnQuestListUpdated?.Invoke();
        }

        internal bool IsQuesTracked(int questID)
        {
            return _activeQuests.Contains(questID);
        }

        internal void RemoveQuest(int questID)
        {
            if (!_activeQuests.Contains(questID)) return;

            _activeQuests.Remove(questID);
            OnQuestListUpdated?.Invoke();
        }
    }
}
