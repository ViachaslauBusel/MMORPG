using NodeEditor.Helpers;
using UnityEngine;

namespace NodeEditor.Data
{
    [System.Serializable]
    public abstract class Node : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private int m_id = 0;
        [SerializeField, HideInInspector]
        // Position of the node on the canvas
        private Vector2 m_position = new Vector2(50.0f, 100.0f);
        [SerializeField, HideInInspector]
        // Manages the connections (links) to nodes from this node
        protected NodeOutputManager m_outputManager = new NodeOutputManager();
        [SerializeField, HideInInspector]
        private NodesContainer m_parentContainer;

        /// <summary>Provides access to the parent Node Container.</summary>
        public NodesContainer ParentContainer => m_parentContainer; 
        public int ID => m_id;
        /// <summary>Gets or sets the position of the node on the canvas</summary>
        public Vector2 Position { get => m_position; set { m_position = value; } }
        /// <summary>
        /// Manager for connections to nodes following this node.
        /// </summary>
        public NodeOutputManager ConnectionManager => m_outputManager;


        public void Initialize(NodesContainer data, int id)
        {
            m_parentContainer = data;
            if (m_id != 0) { Debug.LogError("Failed initialize Node ID"); return; }
            m_id = id;
        }

        public void OnBeforeSerialize()
        {
         //   m_outputManager.PrepareConnectedNodeIDsForSerialization();
        }

        public void OnAfterDeserialize()
        {
            //if(m_parentContainer == null)
            //{
            //    Debug.LogError($"[{GetType()}]:m_parentContainer is null.");
            //}

            // Restores links to nodes in user-defined classes
            // Retrieve all fields of the class
            FieldPortDescriptor[] portDescriptors = FieldPortDescriptor.GetPorts(this);
            foreach (var field in portDescriptors)
            {
                // Find fields marked with the attribute
                var portInfo = field.Attribute;
                if (portInfo == null) continue;

                var port = m_outputManager.GetPortByName(portInfo.Name);
                if (port == null) continue;

                // Restores links to nodes in class field
                if (field.Field.FieldType.IsArray)
                {
                    field.Field.SetValue(this, port.GetAllNodes());
                }
                else
                {
                    object fieldValue = port.ConnectionsCount > 0 ? port.GetConnectedNodeByIndex(0) : null;

                    field.Field.SetValue(this, fieldValue);
                }
            }
        } 
    }
}