using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.CustomInspector;
using NodeEditor.Data;
using Quests;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group: "Dialogues"), NodeDisplayStyle(NodeStyle.Style_2)]
    internal class QuestLevelCheckNode : Node, IConditionNode
    {
        [Port("out")]
        public Node _next;

        [SerializeField]
        private QuestData _questData;
        [SerializeField, Dropdown("GenerateQuestStageDropdownOptions")]//The required node ID (current quest level)
        private int _requiredQuestStageID;

        public int RequiredStageId => _requiredQuestStageID;
        public int QuestId => _questData.ID;
        public Node SuccesNode => _next;
        public Node FailNode => null;

        private DropdownOptionValue[] GenerateQuestStageDropdownOptions()
        {
            if(_questData == null)
            {
                return new DropdownOptionValue[] { new DropdownOptionValue(0, "No quest selected") };
            }

            List<DropdownOptionValue> values = new List<DropdownOptionValue>();
            values.Add(new DropdownOptionValue(0, "Quest not taken"));

            foreach (var node in _questData.Nodes)
            {
                if(node is QuestStageNode stageNode)
                {
                    values.Add(new DropdownOptionValue(stageNode.ID, stageNode.Name));
                }
            }

            values.Add(new DropdownOptionValue(-1, "Quest completed"));
            return values.ToArray();
        }
    }
}
