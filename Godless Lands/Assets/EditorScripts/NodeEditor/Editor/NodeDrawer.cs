using NodeEditor.Attributes;
using NodeEditor.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    public class NodeDrawer
    {
        private const float WIDTH = 300.0f;
        private const float HEIGHT = 20.0f;
        private Node m_node;
        private bool m_isSelected = false;
        private float m_contentHeight;
        private float m_titleHeight;
        private string m_labelTXT = "node";
        private GUIStyle m_style;
        private GUIStyle m_slectedStyle;
        private Rect m_rectTitle = Rect.zero;
        private Rect m_rectBody = Rect.zero;
        private GenericMenu m_menu = new GenericMenu();


        private List<PortDrawer> m_inPorts = new List<PortDrawer>();
        private List<PortDrawer> m_outPorts = new List<PortDrawer>();

        public int InPortsCount => m_inPorts.Count;
        public int OutPortsCount => m_outPorts.Count;

        public PortDrawer GetInPort(int index) => m_inPorts[index];
        public PortDrawer GetOutPort(int index) => m_outPorts[index];

        public Node Node => m_node;

        private NodeDrawer(Node node)
        {
            m_node = node;
            // necessary to limit the length of the text, otherwise it goes beyond the window
            m_labelTXT = m_node.GetType().Name.Length > 25 ? $"{m_node.GetType().Name.Substring(0, 25)}.:{m_node.ID}" : $"{m_node.GetType().Name}:{m_node.ID}";

            // Create the style for the node
            NodeStyle nodeStyle = NodeStyle.Style_0;
            var attribute = m_node.GetType().GetCustomAttribute<NodeDisplayStyleAttribute>();
            if(attribute != null)
            {
                nodeStyle = attribute.DisplayStyle;
            }

            switch (nodeStyle)
            {
                case NodeStyle.Style_0:
                    m_style = RedactorStyle.PlayerDialogueTitle;
                    m_slectedStyle = RedactorStyle.PlayerDialogueTitleSelect;
                    break;
                case NodeStyle.Style_1:
                    m_style = RedactorStyle.NPCDialogueTitle;
                    m_slectedStyle = RedactorStyle.NPCDialogueTitleSelect;
                    break;
                case NodeStyle.Style_2:
                    m_style = RedactorStyle.ActionTitle;
                    m_slectedStyle = RedactorStyle.ActionTitleSelect;
                    break;
                default:
                    m_style = RedactorStyle.PlayerDialogueTitle;
                    m_slectedStyle = RedactorStyle.PlayerDialogueTitleSelect;
                    break;
            }
        }

        public void AddPort(PortDrawer port)
        {
            if (port.PortType == PortType.IN) { m_inPorts.Add(port); }
            else if (port.PortType == PortType.OUT) { m_outPorts.Add(port); }
            else Debug.LogError("Error adding port to NodeDrawer: Invalid port type");
        }

        /// <summary>
        /// Creates and configures the wrapper for node drawing.
        /// </summary>
        /// <param name="windowNodeEditor">The editor window containing the node.</param>
        /// <param name="node">The node to be drawn.</param>
        /// <returns>The created NodeDrawer.</returns>
        public static NodeDrawer Create(WindowNodeEditor windowNodeEditor, Node node)
        {
            NodeDrawer nodeDrawer = new NodeDrawer(node);

            // Add context menu item to remove the node if it is not a StartNode
            if (!(node is StartNode))
            {
                nodeDrawer.AddItemContextMenu("Remove this Node", () => windowNodeEditor.RemoveNode(node.ID));
            }

            // Add input port
            nodeDrawer.AddPort(new PortDrawer(nodeDrawer, PortType.IN));

            // Add output ports based on the node's ConnectionManager
            for (int port = 0; port < node.ConnectionManager.PortsCount; port++)
            {
                string portName = node.ConnectionManager.GetPortByIndex(port).Name;
                nodeDrawer.AddPort(new PortDrawer(nodeDrawer, PortType.OUT, port, portName));
            }

            return nodeDrawer;
        }


        public void AddItemContextMenu(string content, GenericMenu.MenuFunction function)
        {
            m_menu.AddItem(new GUIContent(content), false, function);
        }

        public void Draw(ZoomArea zoomArea) 
        {
            // Set up the rectangles for the title and body of the node
            m_rectTitle = new Rect(m_node.Position.x, m_node.Position.y, WIDTH, m_titleHeight + HEIGHT);
            m_rectBody = new Rect(m_node.Position.x, m_node.Position.y, WIDTH, m_contentHeight + m_titleHeight + 10.0f);

            //Body
            GUILayout.BeginArea(m_rectBody, RedactorStyle.StageBody);
            {
                float border = 10.0f;

                // Create an area for the body content
                GUILayout.BeginArea(new Rect(border, border*2.0f, m_rectBody.width - border * 2, m_rectBody.height - border * 2));
                GUILayout.BeginVertical();

                // Space for the title
                GUILayout.Space(m_titleHeight);

                // Draw the default inspector for the node
                Editor defaultEditor = NodeEditor.Inspector.NodeContentRenderer.CreateEditor(m_node, typeof(NodeEditor.Inspector.NodeContentRenderer));
                defaultEditor.OnInspectorGUI();

                GUILayout.EndVertical();

                // Get the height of the body content
                if (Event.current.type == EventType.Repaint)
                {
                    m_contentHeight = GUILayoutUtility.GetLastRect().height;
                }

                GUILayout.EndArea();
            }
            GUILayout.EndArea();

            //Title
            GUIStyle titleStyle = m_isSelected ? m_slectedStyle : m_style;

            GUILayout.BeginArea(m_rectTitle, titleStyle);
            {
                GUILayout.BeginVertical();
                {
                    GUILayout.Space(15.0f);
                    for (int i = 0; i < Mathf.Max(m_inPorts.Count, m_outPorts.Count); i++)
                    {
                        GUILayout.BeginHorizontal();

                        // Draw input port if available
                        if (i < m_inPorts.Count)
                        {
                            m_inPorts[i].Draw();
                        }

                        GUILayout.FlexibleSpace();

                        // Draw label
                        if (i == 0)
                        {
                            GUILayout.Label(m_labelTXT);
                            GUILayout.FlexibleSpace();
                        }

                        // Draw output port if available
                        if (i < m_outPorts.Count)
                        {
                            m_outPorts[i].Draw();
                        }

                        GUILayout.EndHorizontal();
                    }
        
                }
                GUILayout.EndVertical();

                // Get the height of the title content
                if (Event.current.type == EventType.Repaint)
                {
                    m_titleHeight = GUILayoutUtility.GetLastRect().height;
                }
            }
            GUILayout.EndArea();
        }

        public void ProcessEvents(Event e)
        {
            // Process events for input and output ports
            foreach (PortDrawer port in m_inPorts) { port.ProcessEvents(e); }
            foreach (PortDrawer port in m_outPorts) { port.ProcessEvents(e); }

            // If the mouse cursor is inside the window
            if (m_rectTitle.Contains(e.mousePosition))
            {
                // If the left mouse button is pressed, select the window
                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    m_isSelected = true;
                    GUI.changed = true;
                    // Consume the event
                    e.Use();
                }
                // If the right mouse button is pressed, show context menu
                else if (e.type == EventType.MouseDown && e.button == 1)
                {
                    m_menu.ShowAsContext();
                    e.Use();
                }
            }

            // If the mouse button is released, deselect the window
            if (e.type == EventType.MouseUp)
            {
                m_isSelected = false;
                GUI.changed = true;
            }
            // If the mouse is being dragged with the left button and the window is selected, move the window
            else if (e.type == EventType.MouseDrag && e.button == 0 && m_isSelected)
            {
                m_node.Position += e.delta;
                GUI.changed = true;
            }
        }
    }
}