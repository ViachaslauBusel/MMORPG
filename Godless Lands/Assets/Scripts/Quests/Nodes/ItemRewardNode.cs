using NodeEditor.Attributes;
using NodeEditor;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Items;
using Quests.Export;
using Protocol.Data.Quests;
using Protocol.Data.Items;
using Protocol.Data.Quests.Nodes;

namespace Quests.Nodes
{
    [NodeGroup("Quests"), NodeDisplayStyle(NodeStyle.Style_2)]
    internal class ItemRewardNode : Node, IHaveNextNode, IExportableNode
    {
        [Port("succes")]
        private Node _successNode;
        [Port("fail")]
        private Node _failNode;
        [SerializeField]
        private List<ItemBundle> _itemsReward;

        public int SuccesIdNode => _successNode != null ? _successNode.ID : 0;
        public Node NextNode => _successNode;

        public QuestSNode ToServerData()
        {
            var rewards = _itemsReward != null ? _itemsReward.Select(i => new ItemBundleSData(i.ID, i.Amount)).ToList() : new List<ItemBundleSData>();
            return new ItemRewardSNode(ID, _successNode?.ID ?? 0, rewards);
        }
    }
}
