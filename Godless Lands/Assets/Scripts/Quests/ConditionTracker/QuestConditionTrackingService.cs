using Quests.ConditionTracker.UI;
using Quests.Journal;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quests.ConditionTracker
{
    internal class QuestConditionTrackingService : MonoBehaviour
    {
        private QuestJournalService m_questJournalService;
        private QuesTrackingUI m_questTrackingUI;
        private Dictionary<int, ITrackableQuestCondition> m_currentTrackableQuestStages = new();
        private List<int> m_questsToUpdate = new();

        [Inject]
        private void Construct(QuestJournalService questJournalService, QuesTrackingUI quesTrackingUI)
        {
            m_questJournalService = questJournalService;
            m_questTrackingUI = quesTrackingUI;
        }

        private void Start()
        {
            m_questJournalService.OnJournalChanged += OnJournalChanged;
            m_questJournalService.OnQuestChanged += OnQuestChanged;
            LoadQuestJournal();
        }

        // Called when the journal is updated, including when the ID of the tracked quest changes
        private void OnJournalChanged()
        {
            UpdateUI();
        }

        // Called when the quest state changes
        private void OnQuestChanged(Quest quest)
        {
            QuestStageNode questStageNode = (QuestStageNode)quest.Data.GetNodeByID(quest.CurrentStageID);
            UpdateStageFor(quest.ID, questStageNode);

            if(quest.ID == m_questJournalService.TrackingQuestID)
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (m_currentTrackableQuestStages.ContainsKey(m_questJournalService.TrackingQuestID))
            {
                m_questTrackingUI.UpdateInformation(m_currentTrackableQuestStages[m_questJournalService.TrackingQuestID]);
            }
            else
            {
                m_questTrackingUI.HideInformation();
            }
        }

        // Loads all quests that need to track their state
        private void LoadQuestJournal()
        {
            foreach(var quest in m_questJournalService.Quests)
            {
                QuestStageNode questStageNode = (QuestStageNode)quest.Data.GetNodeByID(quest.CurrentStageID);

                UpdateStageFor(quest.ID, questStageNode);
            }

            if(m_currentTrackableQuestStages.ContainsKey(m_questJournalService.TrackingQuestID))
            {
                m_questTrackingUI.UpdateInformation(m_currentTrackableQuestStages[m_questJournalService.TrackingQuestID]);
            }
        }

        // Adds a new quest to the list of tracked quests if it needs to be tracked
        private void UpdateStageFor(int questID, QuestStageNode questStageNode)
        {
            // If the quest is already being tracked, stop tracking it but temporarily do not remove it from the list
            bool isQuestTracked = m_currentTrackableQuestStages.ContainsKey(questID);
            if (isQuestTracked)
            {
                m_currentTrackableQuestStages[questID].StopTracking();
                m_currentTrackableQuestStages[questID] = null;
            }

            // If the quest needs to be tracked, add it to the lis
            if (questStageNode != null && questStageNode is ITrackableQuestCondition trackableQuestCondition)
            {
                Debug.Log($"Start tracking quest {questID}");
                if (!isQuestTracked) 
                {
                    m_currentTrackableQuestStages.Add(questID, trackableQuestCondition);
                }
                else
                {
                    m_currentTrackableQuestStages[questID] = trackableQuestCondition;
                }
                m_currentTrackableQuestStages[questID].StartTracking();
            }
            // If the quest does not need to be tracked and it was being tracked, remove it from the list
            else if (isQuestTracked)
            {
                m_currentTrackableQuestStages.Remove(questID);
            }
        }

        private void Update()
        {
            foreach (var quest in m_currentTrackableQuestStages)
            {
                quest.Value.UpdateTracking(out bool isStateUpdated, out bool isConditionConfirmed);

                // If the quest state has changed, update the UI
                if (isStateUpdated)
                {
                    m_questTrackingUI.UpdateInformation(quest.Value);
                }
                // If the quest condition has been confirmed, move to the next stage
                if (isConditionConfirmed)
                {
                    m_questsToUpdate.Add(quest.Key);
                }
            }
            foreach (var questID in m_questsToUpdate)
            {
                m_questJournalService.IncreaseQuestStage(questID);
            }
            m_questsToUpdate.Clear();
        }

        private void OnDestroy()
        {
            m_questJournalService.OnJournalChanged -= LoadQuestJournal;
        }
    }
}
