using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Data
{
    public interface IHaveNextNode
    {
        Node NextNode { get; }
    }
}
