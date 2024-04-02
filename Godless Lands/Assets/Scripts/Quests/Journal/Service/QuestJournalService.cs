using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Quests.Journal
{
    public class QuestJournalService : MonoBehaviour
    {
        private Dictionary<int, QuestHandler> m_journal = new();
        private int m_trackingQuestID = 0;
        private int m_activeQuestID = 0;
      
        public event Action OnJournalChanged;
        public event Action<Quest> OnQuestChanged;

        public int QuestCount => m_journal.Count;
        public IEnumerable<Quest> Quests => m_journal.Values.Select(p => p.GetQuest());
        /// <summary>
        /// The ID of the quest that is currently being tracked
        /// </summary>
        public int TrackingQuestID => m_trackingQuestID;
        /// <summary>
        /// The ID of the quest that is currently selected in journal UI
        /// </summary>
        public int ActiveQuestID => m_activeQuestID;

        public void SetTrackedQuest(int questID)
        {
            m_trackingQuestID = questID;
            OnJournalChanged?.Invoke();
        }

        public void SetActiveQuest(int questID)
        {
            m_activeQuestID = questID;
            OnJournalChanged?.Invoke();
        }

        public bool TryGetQuestByData(QuestData data, out Quest quest)
        {
            quest = default;
            if (data == null || !m_journal.TryGetValue(data.GetInstanceID(), out var questHandler))
            {
                return false;
            }

            quest = questHandler.GetQuest();
            return true;
        }

        public bool TryGetQuestByID(int questID, out Quest quest)
        {
            return TryGetQuestFrom(m_journal.Values.FirstOrDefault(x => x.ID == questID), out quest);
        }

        public bool TryGetQuestByIndex(int questIndex, out Quest quest)
        {
            quest = default;
            if (questIndex < 0 || questIndex >= m_journal.Count) return false;

            return TryGetQuestFrom(m_journal.Values.ElementAt(questIndex), out quest);
        }

        public bool IncreaseQuestStage(QuestData questData)
        {
            QuestHandler questHandler = GetQuestHandler(questData);
            return IncreaseQuestStage(questHandler);
        }

        // TODO This method is not safe, as it requires the quest to already be in the journal.
        // We need to create a common database that will provide quest data by ID.
        public bool IncreaseQuestStage(int questID)
        {
           
            if(TryGetQuestByID(questID, out var quest))
            {
                return IncreaseQuestStage(quest.Data);
            }

            Debug.LogError($"IncreaseQuestStage: Quest with ID {questID} does not exist");
            return false;
        }

        private bool TryGetQuestFrom(QuestHandler questHandler, out Quest quest)
        {
            quest = questHandler?.GetQuest() ?? default;
            return questHandler != null;
        }

        private bool IncreaseQuestStage(QuestHandler questHandler)
        {
            bool questUpResult = questHandler.UpQuestStage();

            if (questUpResult)
            {
                SetTrackedQuest(questHandler.ID);
                OnQuestChanged?.Invoke(questHandler.GetQuest());
                OnJournalChanged?.Invoke();
            }
            return questUpResult;
        }

        private QuestHandler GetQuestHandler(QuestData quest)
        {
            int questDataId = quest.GetInstanceID();
            QuestHandler questHandler;
            if (!m_journal.ContainsKey(questDataId))
            {
                questHandler = new QuestHandler(questDataId, quest);
                m_journal.Add(questDataId, questHandler);
            }
            else
            {
                questHandler = m_journal[questDataId];
            }

            return questHandler;
        }
    }
}
