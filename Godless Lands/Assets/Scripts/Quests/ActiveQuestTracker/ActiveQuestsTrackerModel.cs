using Network;
using System;
using System.Collections.Generic;

namespace Quests.ActiveQuestTracker
{
    public class ActiveQuestsTrackerModel
    {
        private SessionManagmentService _sessionManagmentService;
        private Dictionary<int, bool> _quests = new();
        private List<int> _activeQuests = new();


        public Action<int> OnQuestStartTracking;
        public Action OnQuestListUpdated;

        public IReadOnlyCollection<int> ActiveQuests => _activeQuests;
        public IReadOnlyCollection<int> Quests => _quests.Keys;

        internal void AddQuest(int questId, bool isActive = true)
        {
            if (_quests.ContainsKey(questId)) return;
            _quests.Add(questId, isActive);
            UpdateActiveQuests();
            OnQuestStartTracking?.Invoke(questId);
        }

        private void UpdateActiveQuests()
        {
            _activeQuests.Clear();
            foreach (var quest in _quests)
            {
                if (quest.Value)
                {
                    _activeQuests.Add(quest.Key);
                }
            }
            OnQuestListUpdated?.Invoke();
        }

        internal bool IsQuestTracked(int questID)
        {
            return _quests.ContainsKey(questID) && _quests[questID];
        }

        internal void RemoveQuest(int questID)
        {
            if (!_quests.ContainsKey(questID)) return;

            _quests.Remove(questID);
            UpdateActiveQuests();
        }

        internal void SetQuestActive(int questId, bool v)
        {
            if (_quests.ContainsKey(questId))
            {
                _quests[questId] = v;
                UpdateActiveQuests();
            }
        }
    }
}
