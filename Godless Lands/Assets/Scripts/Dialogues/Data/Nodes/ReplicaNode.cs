using NodeEditor;
using NodeEditor.Data;
using Unity;
using UnityEngine;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group: "Dialogues")]
    public class ReplicaNode : Node
    {
        [Port("out"), HideInInspector]
        public Node m_next;
        [SerializeField, Multiline]
        private string m_replica;

        public string Replica => m_replica;
        public Node Next => m_next;
    }
}
