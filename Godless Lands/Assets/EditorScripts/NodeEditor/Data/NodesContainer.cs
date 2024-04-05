using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NodeEditor.Data
{
    [System.Serializable]
    public class NodesContainer : ScriptableObject//, ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<Node> m_nodes = new List<Node>();
        [SerializeField, HideInInspector]
        private int m_generatorID = 2;

        public StartNode StartNode => (StartNode)m_nodes.Find(n => n is StartNode);
        public int NodesCount => m_nodes.Count;
        public IEnumerable<Node> Nodes => m_nodes;
       

        public Node GetNodeByIndex(int i) => m_nodes[i];

        public Node GetNodeByID(int id) => m_nodes.Find((n) => n.ID == id);

        /// <summary>
        /// Generates a unique ID for a node within this container.
        /// </summary>
        /// <returns>A unique node ID.</returns>
        private int GetUniqueID()
        {
            int id = 0;
            do
            {
                id = m_generatorID++;
            } 
            while (m_nodes.Any(n => n.ID == id));

            return id;
        }
#if UNITY_EDITOR

        /// <summary>
        /// Checks for errors in the nodes container.
        /// </summary>
        public void Validate()
        {
            // Ensure that each container has a starting node
            if (StartNode == null)
            {
                // If no starting node is found, add a new one
                AddNode(StartNode.Create());
            }

            for(int i = m_nodes.Count - 1; i >= 0; i--)
            {
                if (m_nodes[i] == null)
                {
                    Debug.LogError($"Error validating a container: a node at index {i} is null");
                    m_nodes.RemoveAt(i);
                    continue;
                }
                for(int j = 0; j < m_nodes[i].ConnectionManager.PortsCount; j++)
                {
                    Port port = m_nodes[i].ConnectionManager.GetPortByIndex(j);
                    if (port == null)
                    {
                        Debug.LogError($"Error validating a container: a port at index {j} of the node at index {i} is null");
                        continue;
                    }

                    for(int k=port.ConnectionsCount-1; k >= 0; k--)
                    {
                        if (port.GetConnectedNodeByIndex(k) == null)
                        {
                            port.RemoveLinkByIndex(k);
                        }
                    }
                }
                NodeHelper.ValidatePortCounts(m_nodes[i]);
            }
        }

        /// <summary>
        /// Save the scriptable object asset include all nodes inside
        /// </summary>
        public void SaveAsset()
        {
            //Debug.Log("SaveAsset");
            foreach (var node in m_nodes) { EditorUtility.SetDirty(node); }
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }

        /// <summary>
        /// Adds a node to the container.
        /// </summary>
        /// <param name="node">The node to be added.</param>
        public void AddNode(Node node)
        {
            // Generate a unique ID for the new node
            int nodeID = GetUniqueID();

            // Initialize the node with the generated ID
            node.Initialize(this, nodeID);

            // Add the node to the asset and the list of nodes
            AssetDatabase.AddObjectToAsset(node, this);
            m_nodes.Add(node);

            // Save the asset to apply the changes
            SaveAsset();
        }

        /// <summary>
        /// Removes a node with the specified ID from the container.
        /// </summary>
        /// <param name="nodeID">The ID of the node to be removed.</param>
        public void RemoveNode(int nodeID)
        {
            // Find the node with the specified ID
            Node removeNode = m_nodes.Find((n) => n.ID == nodeID);

            if (removeNode == null)
            {
                Debug.LogError($"Error removing a node: could not find a node with the specified ID: {nodeID}");
                return;
            }

            // Remove connections to the node being removed from other nodes
            ClearConnections(nodeID);

            // Remove the node from the list and the associated object from the asset
            m_nodes.Remove(removeNode);
            AssetDatabase.RemoveObjectFromAsset(removeNode);

            // Save the asset to apply the changes
            SaveAsset();
        }

        /// <summary>
        /// Clears connections to the node with the specified ID from all nodes in the container.
        /// </summary>
        /// <param name="nodeID">The ID of the node for which connections should be cleared.</param>
        public void ClearConnections(int nodeID)
        {
            // Iterate through all nodes in the container and clear connections to the specified nodeID
            for (int i = 0; i < m_nodes.Count; i++)
            {
                m_nodes[i].ConnectionManager.ClearConnectionsToNode(nodeID);
            }
        }
#endif

        //----------------------- The code below is responsible for serialization/desarilization ---------------------------------
        //private void OnBeforeSerialize()
        //{
        //    foreach (Node node in m_nodes) { node.BeforeSerialize(); }
        //}

        //private void OnAfterDeserialize()
        //{
        //    Debug.Log("OnAfterDeserialize");
        //    foreach (Node node in m_nodes) { node.AfterDeserialize(this); }
        //}

        //void ISerializationCallbackReceiver.OnBeforeSerialize() => OnBeforeSerialize();

        //void ISerializationCallbackReceiver.OnAfterDeserialize() => OnAfterDeserialize();


        //----------------------- The code below is responsible for opening a window by double clicking on an asset (scriptableObject file) ---------------------------------

        //[OnOpenAssetAttribute(1)]
        //public static bool OpenGameStateWindow(int instanceID, int line)
        //{
        //    Debug.Log($"1:instanceID:{instanceID}, line:{line}");
        //    bool windowIsOpen = EditorWindow.HasOpenInstances<WindowNodeEditor>();
        //    if (!windowIsOpen)
        //    {
        //        EditorWindow.CreateWindow<WindowNodeEditor>();
        //    }
        //    else
        //    {
        //        EditorWindow.FocusWindowIfItsOpen<WindowNodeEditor>();
        //    }

        //    // Window should now be open, proceed to next step to open file
        //    return false;
        //}
    }
}