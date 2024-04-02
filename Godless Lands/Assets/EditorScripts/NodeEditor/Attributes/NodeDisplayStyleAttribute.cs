using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeDisplayStyleAttribute : Attribute
    {
        public NodeStyle DisplayStyle { get; private set; }

        public NodeDisplayStyleAttribute(NodeStyle displayStyle)
        {
            DisplayStyle = displayStyle;
        }
    }

    public enum NodeStyle
    {
        Style_0,
        Style_1,
        Style_2,
    }
}
