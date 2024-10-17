using Protocol.MSG.Game.Quests;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestsModel
    {
        private Dictionary<int, Quest> _quests = new Dictionary<int, Quest>();
        private HashSet<int> _completedQuests = new HashSet<int>();
        private QuestRegistry _questRegistry;
      
        public event Action OnJournalUpdated;
        public event Action<Quest, QuestSyncFlag> OnQuestUpdated;

        public IReadOnlyCollection<Quest> Quests => _quests.Values;

        public QuestsModel(QuestRegistry questRegistry)
        {
            _questRegistry = questRegistry;
        }

        internal void UpdateQuest(int questId, int stageID)
        {
            var flag = DetermineFlag(stageID);
            Debug.Log($"UpdateQuest: {questId} {stageID} {flag}");
            if (flag == QuestSyncFlag.Completed)
            {
                if (_quests.ContainsKey(questId))
                {
                    var quest = _quests[questId];
                    _quests.Remove(questId);

                    OnQuestUpdated?.Invoke(quest, flag);
                }

                _completedQuests.Add(questId);
                return;
            }

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

        private QuestSyncFlag DetermineFlag(int stageID)
        {
            if (stageID == -1) return QuestSyncFlag.Completed;
            if (stageID == 0) return QuestSyncFlag.NotStarted;
            return QuestSyncFlag.InProgress;
        }

        internal void NotifyQuestUpdates()
        {
            OnJournalUpdated?.Invoke();
        }

        internal bool TryGetQuestById(int questId, out Quest quest)
        {
            return _quests.TryGetValue(questId, out quest);
        }

        public int GetQuestCurrentStageID(int questId)
        {
            if (_quests.TryGetValue(questId, out Quest quest))
            {
                return quest.CurrentStageID;
            }

            if (_completedQuests.Contains(questId))
            {
                return -1;
            }

            return 0;
        }
    }
}
