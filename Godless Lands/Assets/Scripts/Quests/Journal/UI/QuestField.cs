using Quests;
using Quests.Journal;
using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class QuestField : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_text;
    [SerializeField]
    private Button m_button;

    internal void SetQuest(QuestJournalService questJournal, Quest quest)
    {
        m_text.text = $"{quest.Data.Name} {ConvertStatusToString(questJournal, quest)}";

        m_button.onClick.RemoveAllListeners();
        if (quest.ID == questJournal.ActiveQuestID)
        {
            m_button.interactable = false;
        }
        else
        {
            m_button.interactable = true;
            m_button.onClick.AddListener(() => questJournal.SetActiveQuest(quest.ID));
        }
    }

    private string ConvertStatusToString(QuestJournalService questJournal, Quest quest)
    {
        if(quest.CurrentStageID == -1)
        {
            return "(Completed)";
        }
        if(quest.ID == questJournal.TrackingQuestID)
        {
            return "(Tracking)";
        }

        return "";
    }
}