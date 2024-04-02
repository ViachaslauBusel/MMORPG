using NodeEditor;
using NodeEditor.Attributes;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group:"Dialogues"), NodeDisplayStyle(NodeStyle.Style_1)]
    public class DialogNode : Node
    {
        [Port("npc")]
        private Node m_npcReplicaPort;
        [Port("player", 10)]
        private Node[] m_playerReplica;

        [SerializeField, Multiline]
        private string m_npcReplica;

        public string NpcReplica => m_npcReplica;
        public Node NpcReplicaPort => m_npcReplicaPort;
        public IEnumerable<Node> PlayerReplica => m_playerReplica;
    }
}
