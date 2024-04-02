using NodeEditor.Attributes;
using System.Linq;
using UnityEngine;

namespace NodeEditor.Data
{
    [System.Serializable, NodeDisplayStyle(NodeStyle.Style_2)]
    public class StartNode : Node
    {
        // Output port for connecting to the next node
        [Port("out"), HideInInspector]
        public Node m_next;

        // Property to access the connected next node
        public Node Next => m_next;

        // Private constructor to prevent direct instantiation
        private StartNode() { }

        /// <summary>
        /// Creates a new instance of StartNode.
        /// </summary>
        /// <returns>The created StartNode.</returns>
        public static StartNode Create()
        {
            // Use the NodeHelper to create and initialize a StartNode
            StartNode node = NodeHelper.CreateNode<StartNode>();
            return node;
        }
    }
}