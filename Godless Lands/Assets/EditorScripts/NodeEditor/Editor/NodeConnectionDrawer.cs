using NodeEditor.Data;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    public class NodeConnectionDrawer
    {
        private static PortDrawer m_activeOutPort;

        public static void ActivateOutPort(PortDrawer outPort)
        {
            m_activeOutPort = outPort;
        }

        internal static void TryConnectPorts(PortDrawer inPort)
        {
            if (m_activeOutPort != null)
            {
                m_activeOutPort.Parent.Node.ConnectionManager.GetPortByIndex(m_activeOutPort.Index).AddLink(inPort.Parent.Node);
            }
        }

        public static void Draw(List<NodeDrawer> nodes)
        {
            // Draw connection from the active port to the mouse position
            if (m_activeOutPort != null)
            {
                DrawBezier(m_activeOutPort.Position, Event.current.mousePosition, Color.white);
                GUI.changed = true;

                // Clear the active port on mouse up
                if (Event.current.type == EventType.MouseUp) { m_activeOutPort = null; }
            }

            // Draw connections between nodes
            foreach (NodeDrawer currentNodeDrawer in nodes)
            {
                Node currentNode = currentNodeDrawer.Node;

                // Iterate through ports of the current node
                for (int port = 0; port < currentNode.ConnectionManager.PortsCount; port++)
                {
                    // Iterate through connections of the current port
                    for (int connectionIndex = 0; connectionIndex < currentNode.ConnectionManager.GetPortByIndex(port).ConnectionsCount; connectionIndex++)
                    {
                        // Get the ID of the connected node
                        int nextNodeID = currentNode.ConnectionManager.GetPortByIndex(port).GetConnectedNodeByIndex(connectionIndex).ID;

                        // Find the NodeDrawer for the connected node
                        NodeDrawer nextDrawer = nodes.Find((n) => n.Node.ID == nextNodeID);

                        // Check if the connected node drawer is found
                        if (nextDrawer == null)
                        {
                            Debug.LogWarning("Failed to find the node drawer.");
                            continue;
                        }

                        // Draw connection between the current and connected nodes
                        DrawBezier(currentNodeDrawer.GetOutPort(port), nextDrawer.GetInPort(0), Color.cyan);
                    }
                }
            }
        }

        private static void DrawBezier(PortDrawer portOUT, PortDrawer portIN, Color color) => DrawBezier(portOUT.Position, portIN.Position, color);
        private static void DrawBezier(Vector2 startPosition, Vector2 endPosition, Color color)
        {
            // Draw Bezier curve between start and end positions
            Handles.DrawBezier(
                startPosition,
                endPosition,
                startPosition - Vector2.left * 50f,
                endPosition + Vector2.left * 50f,
                color,
                null,
                4f
            );
        }


    }
}