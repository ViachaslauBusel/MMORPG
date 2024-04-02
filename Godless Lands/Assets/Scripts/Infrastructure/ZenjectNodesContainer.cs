using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ZenjectNodesContainer : NodesContainer
    {
        private void OnEnable()
        {
            foreach (var node in Nodes)
            {
                ZenjectNodeInstaller.InjectDependenciesOnNode(node);
            }
        }
    }
}
