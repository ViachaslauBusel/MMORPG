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
    internal class InventoryItemQuantityCheckNode : Node
    {
        [Port("out")]
        private Node m_nextNode;
    }
}
