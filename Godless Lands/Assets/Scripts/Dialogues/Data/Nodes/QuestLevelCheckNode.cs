using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.CustomInspector;
using NodeEditor.Data;
using Quests;
using Quests.Journal;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group: "Dialogues"), NodeDisplayStyle(NodeStyle.Style_2)]
    internal class QuestLevelCheckNode : Node, IConditionNode
    {
        [Port("out")]
        public Node m_next;

        [SerializeField]
        private QuestData m_questData;
        [SerializeField, Dropdown("GenerateQuestStageDropdownOptions")]//The required node ID (current quest level)
        private int m_requiredQuestStageID;

        private QuestJournalService m_journal;

        [Inject]
        private void Construct(QuestJournalService questJournalService)
        {
            m_journal = questJournalService;
        }

        public Node Next => m_next;

        public bool CheckCondition()
        {
            // Check if this quest exists in the journal
            bool isExist = m_journal.TryGetQuestByData(m_questData, out Quest quest);

            // If the quest does not exist in the journal, then the ID of its current stage is 0
            int questStageID = isExist ? quest.CurrentStageID : 0;

            // Compare the quest stage ID with the required one
            return questStageID == m_requiredQuestStageID;
        }

        private DropdownOptionValue[] GenerateQuestStageDropdownOptions()
        {
            if(m_questData == null)
            {
                return new DropdownOptionValue[] { new DropdownOptionValue(0, "No quest selected") };
            }

            List<DropdownOptionValue> values = new List<DropdownOptionValue>();
            values.Add(new DropdownOptionValue(0, "Quest not taken"));

            foreach (var node in m_questData.Nodes)
            {
                if(node is QuestStageNode stageNode)
                {
                    values.Add(new DropdownOptionValue(stageNode.ID, stageNode.Name));
                }
            }
            return values.ToArray();
        }
    }
}
