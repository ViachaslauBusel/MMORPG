using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Quests.Nodes
{
    [NodeGroup("Quests"), NodeDisplayStyle(NodeStyle.Style_2)]
    internal class NearbyNPCCheck : Node
    {
        [Port("succes")]
        private Node _successNode;
        [Port("fail")]
        private Node _failNode;
        [SerializeField]
        private int _npcID;

        public int SuccesIdNode => _successNode != null ? _successNode.ID : 0;
        public int NPCID => _npcID;
    }
}
