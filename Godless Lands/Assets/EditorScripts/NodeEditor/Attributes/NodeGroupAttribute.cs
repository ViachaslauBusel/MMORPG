using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NodeEditor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeGroupAttribute : Attribute
    {
        public string Group { get; private set; }

        public NodeGroupAttribute(string group)
        {
            Group = group;
        }
    }
}
