using NodeEditor.Data;
using NodeEditor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NodeEditor
{
    public static class NodeHelper
    {
        /// <summary>
        /// Get all nodes of a specified group.
        /// </summary>
        /// <param name="group">The group name to filter nodes. Use an empty string for nodes without a group.</param>
        /// <returns>A list of node types in the specified group.</returns>
        public static List<Type> GetNodesByGroup(string group)
        {
            List<Type> nodes = new List<Type>();

            // Get the type information for the base Node class
            var baseNodeType = typeof(Node);

            // Get all types from the current domain that inherit from Node
            var allNodes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseNodeType.IsAssignableFrom(p))
                .ToArray(); // ToArray to avoid multiple enumerations

            foreach (var nodeType in allNodes)
            {
                // Skip StartNode and the base Node class itself
                if (nodeType == typeof(StartNode) || nodeType == baseNodeType)
                    continue;

                // Retrieve the NodeGroupAttribute from the node type
                var nodeGroupAttribute = nodeType.GetCustomAttribute<NodeGroupAttribute>();
                string nodeGroupName = nodeGroupAttribute?.Group;

                // Check if the node belongs to the specified group
                if (string.IsNullOrEmpty(group))
                {
                    if (string.IsNullOrEmpty(nodeGroupName))
                    {
                        nodes.Add(nodeType);
                    }
                }
                else
                {
                    if (group.Equals(nodeGroupName))
                    {
                        nodes.Add(nodeType);
                    }
                }
            }

            return nodes;
        }

        /// <summary>
        /// Create an instance of a Node based on the specified nodeType and initialize its output ports.
        /// </summary>
        /// <param name="nodeType">The type of Node to create.</param>
        /// <returns>An instance of the specified Node type.</returns>
        public static Node CreateNode(Type nodeType)
        {
            // Create an instance of the specified Node type
            Node node = (Node)ScriptableObject.CreateInstance(nodeType);

            // Retrieve all fields of the Node
            FieldPortDescriptor[] portDescriptors = FieldPortDescriptor.GetPorts(node);

            // Iterate through the fields and initialize output ports based on PortAttribute
            foreach (var descriptor in portDescriptors)
            {
                // Retrieve the PortAttribute from the field
                var portInfo = descriptor.Attribute;

                // Skip fields without PortAttribute
                if (portInfo == null) continue;

                // Determine the number of allowed connections to the port based on information from its attribute
                int count = (descriptor.Field.FieldType.IsArray) ? ((portInfo.Count <= 0) ? 1 : portInfo.Count) : 1;

                // Add the output port to the ConnectionManager
                node.ConnectionManager.AddOutputPort(portInfo.Name, count);
            }

            return node;
        }

        /// <summary>
        /// Creates an instance of a Node with the specified type and initializes its output ports.
        /// </summary>
        /// <typeparam name="T">The type of Node to create.</typeparam>
        /// <returns>An instance of the specified Node type.</returns>
        internal static T CreateNode<T>() where T : Node
        {
            return (T)CreateNode(typeof(T));
        }

        public static void ValidatePortCounts(Node node)
        {
            Type nodeType = node.GetType();

            // Retrieve all portAttributes from the fields of node type
            FieldPortDescriptor[] portDescriptors = FieldPortDescriptor.GetPorts(node);

            // Remove all ports from ConnectionManager that do not have corresponding fields in the class
            for (int i = node.ConnectionManager.PortsCount - 1; i >= 0; i--)
            {
                var port = node.ConnectionManager.GetPortByIndex(i);
                if (!portDescriptors.Any(p => p.Attribute.Name == port.Name))
                {
                    node.ConnectionManager.RemovePortAt(i);
                }
            }

            // Add ports that have fields in the class but not exist in ConnectionManager and update the number of connections if it has changed
            foreach (FieldPortDescriptor descriptor in portDescriptors)
            {
                var portInfo = descriptor.Attribute;

                // Determine the number of allowed connections to the port based on information from its attribute
                int count = (descriptor.Field.FieldType.IsArray) ? ((portInfo.Count <= 0) ? 1 : portInfo.Count) : 1;

                var port = node.ConnectionManager.GetPortByName(portInfo.Name);
                if (port == null)
                {
                    // Add the output port to the ConnectionManager
                    node.ConnectionManager.AddOutputPort(portInfo.Name, count);
                }
                else if (port.MaxConnection != count)
                {
                    port.SetMaxConnectionsCount(count);
                }
            }
        }
    }
}
