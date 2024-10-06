using Items;
using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.Data;
using Protocol.Data.Items;
using Protocol.Data.Quests;
using Protocol.Data.Quests.Nodes;
using Quests.Export;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests.Nodes
{
    [NodeGroup("Quests"), NodeDisplayStyle(NodeStyle.Style_2)]
    internal class InventoryItemRequirementNode : Node, IExportableNode
    {
        [Port("succes")]
        private Node _successNode;
        [Port("fail")]
        private Node _failNode;
        [SerializeField]
        private string _conditionName;
        [SerializeField]
        private ItemBundleLink _requiredItem;

        public int SuccesIdNode => _successNode != null ? _successNode.ID : 0;
        public string ConditionName => _conditionName;
        public Node NextNode => _successNode;
        public int RequiredItemAmount => _requiredItem.Amount;
        public int RequiredItemId => _requiredItem.ID;

        public QuestSNode ToServerData()
        {
            return new InventoryItemRequirementSNode(ID, _successNode.ID, RequiredItemId, RequiredItemAmount);
        }
    }
}
