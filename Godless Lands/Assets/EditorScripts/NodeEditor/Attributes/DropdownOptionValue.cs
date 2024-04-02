using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.CustomInspector
{
    public class DropdownOptionValue
    {
        private int m_index;
        private string m_name;

        public int Value => m_index;
        public string Name => m_name;

        public DropdownOptionValue(int value, string name)
        {
            m_index = value;
            m_name = name;
        }
    }
}
