using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.Data;
using NPCs;
using ObjectRegistryEditor;
using Protocol.Data.Quests;
using Protocol.Data.Quests.Nodes;
using Quests.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Quests.Nodes
{
    [NodeGroup("Quests"), NodeDisplayStyle(NodeStyle.Style_2)]
    internal class NearbyNPCValidatorNode : Node, IHaveNextNode, IExportableNode
    {
        [Port("succes")]
        private Node _successNode;
        [Port("fail")]
        private Node _failNode;
        [SerializeField]
        private DataLink<NPCData> _npc;

        public int SuccesIdNode => _successNode != null ? _successNode.ID : 0;
        public int NPCID => _npc.ID;
        public Node NextNode => _successNode;

        public QuestSNode ToServerData()
        {
            return new NearbyNPCValidatorSNode(ID, _successNode.ID, _npc.ID);
        }
    }
}
