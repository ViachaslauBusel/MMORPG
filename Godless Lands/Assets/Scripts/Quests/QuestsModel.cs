using Protocol.MSG.Game.Quests;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestsModel
    {
        private Dictionary<int, Quest> _quests = new Dictionary<int, Quest>();
        private QuestRegistry _questRegistry;
      
        public event Action OnJournalUpdated;
        public event Action<Quest, QuestSyncFlag> OnQuestUpdated;

        public IReadOnlyCollection<Quest> Quests => _quests.Values;

        public QuestsModel(QuestRegistry questRegistry)
        {
            _questRegistry = questRegistry;
        }

        internal void UpdateQuest(int questId, int stageID, QuestSyncFlag flag)
        {
            if (_quests.ContainsKey(questId))
            {
                _quests[questId].UpdateStageId(stageID);
            }
            else
            {
                QuestData questData = _questRegistry.GetObjectByID(questId);
                if(questData != null) 
                {
                    _quests.Add(questId, new Quest(questId, questData, stageID));
                }
                else Debug.LogError("QuestData not found for questId: " + questId);
            }

            OnQuestUpdated?.Invoke(_quests[questId], flag);
        }

        internal void NotifyQuestUpdates()
        {
            OnJournalUpdated?.Invoke();
        }

        internal bool TryGetQuestById(int questId, out Quest quest)
        {
            return _quests.TryGetValue(questId, out quest);
        }
    }
}
