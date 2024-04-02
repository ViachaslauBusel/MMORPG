using NodeEditor;
using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.Nodes
{
    [NodeGroup("Quests")]
    internal class InventoryItemAvailabilityCheckNode : Node
    {
        [Port("out")]
        private Node m_nextNode;
        [Port("return")]
        private Node m_returnNode;
    }
}
