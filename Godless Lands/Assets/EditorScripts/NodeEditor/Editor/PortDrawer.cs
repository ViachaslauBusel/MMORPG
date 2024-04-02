using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    public class PortDrawer
    {
        private PortType m_portType;
        private Rect m_rect;
        private GUIStyle m_style;
        private string m_name;

        public PortType PortType => m_portType;
        public Vector2 Position { get; private set; }
        public NodeDrawer Parent { get; private set; }
        public int Index { get; private set; }

        public PortDrawer(NodeDrawer parent, PortType type, int portIndex = 0, string name = "")
        {
            Parent = parent;
            m_portType = type;
            Index = portIndex;
            m_name = string.IsNullOrEmpty(name) ? type.ToString() : name;
            m_style = m_portType switch
            { 
                PortType.IN => RedactorStyle.InPoint,
                _ => RedactorStyle.OutPoint
            };
        }

        public void Draw(float positionX, float positionY, float width, float height)
        {
            m_rect = new Rect(positionX, positionY, width, height);
            Position = m_rect.center;
            GUI.Label(m_rect, m_style.normal.background);
        } 
        public void Draw()
        {
            if(m_portType == PortType.OUT) GUILayout.Label(m_name,  GUILayout.Height(20));
            GUILayout.Label(m_style.normal.background, GUILayout.Width(20), GUILayout.Height(20));
            
            if (Event.current.type == EventType.Repaint)
            {
                m_rect = GUILayoutUtility.GetLastRect();
                Vector2 worldPosition = ZoomArea.GetCanvasPosition(GUIUtility.GUIToScreenPoint(Vector2.zero));
                Position = worldPosition + m_rect.center;
                m_rect = new Rect(worldPosition + m_rect.position, m_rect.size);
            }
            if (m_portType == PortType.IN) GUILayout.Label(m_name, GUILayout.Height(20));
        }

        /// <summary>
        /// Process input events for the port.
        /// </summary>
        /// <param name="e">The input event.</param>
        internal void ProcessEvents(Event e)
        {
            // If it's an output port
            if (m_portType == PortType.OUT)
            {
                // Part 1: Activate the outgoing port on mouse down within its bounds
                if (e.type == EventType.MouseDown && m_rect.Contains(e.mousePosition))
                {
                    NodeConnectionDrawer.ActivateOutPort(this);
                    e.Use();
                }
            }
            else // If it's an input port
            {
                // Part 2: Try to connect two ports if the outgoing port was activated
                if (e.type == EventType.MouseUp && m_rect.Contains(e.mousePosition))
                {
                    NodeConnectionDrawer.TryConnectPorts(this);
                }
                // Remove connection to this port on mouse down within its bounds
                else if (e.type == EventType.MouseDown && m_rect.Contains(e.mousePosition))
                {
                    Parent.Node.ParentContainer.ClearConnections(Parent.Node.ID);
                }
            }
        }
    }
    public enum PortType { IN, OUT }
}