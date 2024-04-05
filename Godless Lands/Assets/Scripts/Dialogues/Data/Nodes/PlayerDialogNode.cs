using NodeEditor;
using NodeEditor.Data;
using Unity;
using UnityEngine;

namespace Dialogues.Data.Nodes
{
    [NodeGroup(group: "Dialogues")]
    public class PlayerDialogNode : Node
    {
        [Port("out"), HideInInspector]
        public Node _next;
        [SerializeField, Multiline]
        private string _replica;

        public string Replica => _replica;
        public Node Next => _next;
    }
}
