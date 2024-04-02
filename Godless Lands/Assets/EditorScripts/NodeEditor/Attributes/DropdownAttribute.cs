using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.CustomInspector
{
    public class DropdownAttribute : Attribute
    {
        public string Function { get; }

        public DropdownAttribute(string function)
        {
            Function = function;
        }
    }
}
