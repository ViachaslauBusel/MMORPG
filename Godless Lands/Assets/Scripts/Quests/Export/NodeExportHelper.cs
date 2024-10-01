using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.Export
{
    public static class NodeExportHelper
    {
        public static IExportableNode ToExportableNode(this Node node)
        {
            if (node is IExportableNode exportableNode)
            {
                return exportableNode;
            }
            if (node is StartNode startNode)
            {
                return new StartNodeExport(startNode);
            }
            return null;
        }
    }
}
