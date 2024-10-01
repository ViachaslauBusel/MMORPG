using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.Nodes.Handlers
{
    public interface INodeHandler
    {
        event Action OnTaskProgressChanged;

        Node GetNextNode(Node node);
        string GetDescription(Node node);
        string GetProgress(Node node);
    }
}
