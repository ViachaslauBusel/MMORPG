using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quests.Journal.UI
{
    internal class QuestDescriptionUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_textBlockPrefab;
        private QuestJournalService m_questJournalService;
        private DiContainer m_container;
        private List<TextBlock> m_questLog = new List<TextBlock>();

        [Inject]
        private void Construct(DiContainer container, QuestJournalService questJournalService)
        {
            m_questJournalService = questJournalService;
            m_container = container;
        }

        private void OnEnable()
        {
            Debug.Log("QuestDescriptionUI enabled");
            m_questJournalService.OnJournalChanged += Reload;
            Reload();
        }
        private void OnDisable()
        {
            Debug.Log("QuestDescriptionUI disabled");
            m_questJournalService.OnJournalChanged -= Reload;
        }
        private void Reload()
        {
            //Debug.Log("QuestDescriptionUI Reload");
            int logIndex = 0;

            if(!m_questJournalService.TryGetQuestByID(m_questJournalService.ActiveQuestID, out Quest quest))
            {
                FinishWorkWithTextBlocks(logIndex);
                Debug.LogWarning("No active quest");
                return;
            }

            TextBlock textBlock = GetTextBlock(logIndex++, Color.white);
            textBlock.AddLine($"<b>Name:</b> {quest.Data.Name}");
            textBlock.AddLine("");
            textBlock.AddLine($"<b>Description:</b>\n {quest.Data.Description}");

            foreach(var stage in quest.StagesLog)
            {
                QuestStageNode stageNode = quest.Data.GetNodeByID(stage) as QuestStageNode;
                if (stageNode == null)
                {
                    Debug.LogError($"Could not find node for stage:{stage}");
                }

                textBlock = GetTextBlock(logIndex++, Color.gray);
                textBlock.AddLine($"<b>Stage {stageNode.name}:</b>\n {stageNode.Description}");
            }

            QuestStageNode currentNode = quest.Data.GetNodeByID(quest.CurrentStageID) as QuestStageNode;

            // Current quest level can be null if the quest was completed
            if(currentNode != null)
            {
                textBlock = GetTextBlock(logIndex++, Color.white);
                textBlock.AddLine($"<b>Current stage {currentNode.name}:</b>\n {currentNode.Description}");
            }

            FinishWorkWithTextBlocks(logIndex);
        }

        private TextBlock GetTextBlock(int index, Color color)
        {
            TextBlock textBlock;
            if (index >= m_questLog.Count)
            {
                GameObject obj = m_container.InstantiatePrefab(m_textBlockPrefab, m_textBlockPrefab.transform.parent);
                textBlock = obj.GetComponent<TextBlock>();
                m_questLog.Add(textBlock);
            }
            else
            {
                textBlock = m_questLog[index];
            }

            textBlock.gameObject.SetActive(true);
            textBlock.Clear();
            textBlock.SetColor(color);
            return textBlock;
        }
        private void FinishWorkWithTextBlocks(int index)
        {
            for(int i = index; i < m_questLog.Count; i++)
            {
                m_questLog[i].gameObject.SetActive(false);
            }
        }
    }
}
