using NodeEditor;
using NodeEditor.Data;
using Quests.Journal;
using UnityEngine;
using Zenject;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group: "Dialogues")]
    internal class QuestLevelUpNode : Node, IExecutionNode
    {
        [Port("succes")]
        private Node m_succesNode;
        [Port("fail")]
        private Node m_failNode;
       

        [SerializeField]
        private QuestData m_quest;

        private QuestJournalService m_journal;
        private Node m_next = null;

        public Node Next => m_next;


        [Inject]
        private void Construct(QuestJournalService questJournalService)
        {
            m_journal = questJournalService;
        }

        public void Execute()
        {
            bool questUpResult = m_journal.IncreaseQuestStage(m_quest);
            m_next = questUpResult ? m_succesNode : m_failNode;
        }
    }
}
