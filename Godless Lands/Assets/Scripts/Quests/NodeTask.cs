using NodeEditor.Data;
using Quests.Nodes.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests
{
    public struct NodeTask
    {
        private INodeHandler _handler;
        private Node _node;

        public INodeHandler Handler => _handler;
        public Node Node => _node;

        public NodeTask(INodeHandler handler, Node node)
        {
            _handler = handler;
            _node = node;
        }

        internal string GetDescription() => _handler.GetDescription(_node);
        

        internal string GetProgress() => _handler.GetProgress(_node);
    }
}
