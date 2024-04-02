using NodeEditor.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NodeEditor.Helpers
{
    internal class FieldPortDescriptor
    {
        public FieldInfo Field { get; private set; }
        public PortAttribute Attribute { get; private set; }

        public FieldPortDescriptor(FieldInfo field, PortAttribute attribute)
        {
            Field = field;
            Attribute = attribute;
        }

        internal static FieldPortDescriptor[] GetPorts(Node node)
        {
            List<FieldPortDescriptor> ports = new List<FieldPortDescriptor>();

            var type = node.GetType();
            while (type != typeof(Node))
            {
                var portFields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                         .Select(f => new FieldPortDescriptor(f, f.GetCustomAttribute<PortAttribute>()))
                                         .Where(p => p.Attribute != null);
                ports.AddRange(portFields);
                type = type.BaseType;
            }

            return ports.ToArray();
        }
    }
}
