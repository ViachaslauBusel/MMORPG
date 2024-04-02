using Quests;
using Quests.Journal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestListUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_questFieldPrefab;
    private QuestJournalService m_questJournalService;
    private DiContainer m_container;
    private List<QuestField> m_questList = new List<QuestField>();

    [Inject]
    private void Construct(QuestJournalService questJournalService, DiContainer diContainer)
    {
        m_questJournalService = questJournalService;
        m_container = diContainer;
    }

    private void OnEnable()
    {
        m_questJournalService.OnJournalChanged += Reload;
        Reload();
    }

    private void OnDisable()
    {
        m_questJournalService.OnJournalChanged -= Reload;
    }

    internal void Reload()
    {
        int questIndex = 0;
        //Creates quest fields for all quests in the journal.
        for(int i = 0; i < m_questJournalService.QuestCount; i++)
        {
            // If the quest is not exist in journal or not renderable skip it
            if (!m_questJournalService.TryGetQuestByIndex(i, out Quest quest) || !quest.Data.IsRenderableInJournal)
            {
                continue;
            }
            GetQuestField(questIndex++).SetQuest(m_questJournalService, quest);
        }

        FinishWorkWithQuestField(questIndex);
    }

    private QuestField GetQuestField(int index)
    {
        if (index >= m_questList.Count)
        {
            GameObject questField = m_container.InstantiatePrefab(m_questFieldPrefab, m_questFieldPrefab.transform.parent);
            questField.SetActive(true);
            QuestField questFieldComponent = questField.GetComponent<QuestField>();
            m_questList.Add(questFieldComponent);
            return questFieldComponent;
        }

        m_questList[index].gameObject.SetActive(true);
        return m_questList[index];
    } 

    private void FinishWorkWithQuestField(int index)
    {
        for (int i = index; i < m_questList.Count; i++)
        {
            m_questList[i].gameObject.SetActive(false);
        }
    }
}
