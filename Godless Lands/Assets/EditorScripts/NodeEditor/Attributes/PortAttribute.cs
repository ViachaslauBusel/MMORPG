using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NodeEditor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PortAttribute : Attribute
    {
        public string Name { get; private set; }
        public int Count { get; private set; } = -1;

        public PortAttribute(string name, int count = 1)
        {
            Name = name;
            Count = count;
        }   
    }
}
