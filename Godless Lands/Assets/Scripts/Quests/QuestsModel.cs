using Quests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Quests
{
    internal class QuestsModel
    {
        private Dictionary<int, Quest> _quests = new Dictionary<int, Quest>();
        private QuestDataHolder _dataHolder;
      
        public event Action OnQuestsUpdated;

        public IReadOnlyCollection<Quest> Quests => _quests.Values;

        public QuestsModel(QuestDataHolder dataHolder)
        {
            _dataHolder = dataHolder;
        }

        internal void UpdateQuest(int questId, int stageID)
        {
            if (_quests.ContainsKey(questId))
            {
                _quests[questId].UpdateStageId(stageID);
            }
            else
            {
                QuestData questData = _dataHolder.GetQuestData(questId);
                if(questData != null) 
                {
                    _quests.Add(questId, new Quest(questId, questData, stageID));
                }
                else Debug.LogError("QuestData not found for questId: " + questId);
            }
        }

        internal void NotifyQuestUpdates()
        {
            OnQuestsUpdated?.Invoke();
        }

        internal bool TryGetQuestById(int questId, out Quest quest)
        {
            return _quests.TryGetValue(questId, out quest);
        }
    }
}
