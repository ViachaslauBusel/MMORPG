using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeEditor.Data
{
    /// <summary>
    /// Output port through which the connection with the next nodes is established.
    /// </summary>
    [System.Serializable]
    public class Port
    {
        [SerializeField] string m_name;
        /// <summary>
        /// Maximum number of connections allowed on this port
        /// </summary>
        [SerializeField] int m_maxConnection;
        ///// <summary>
        ///// Array of node IDs representing the connections to other nodes. Used solely for data serialization.
        ///// </summary>
        //[SerializeField] int[] m_nextNodesID;

        [SerializeField] List<Node> m_nextNodes = new List<Node>();

        /// <summary>
        /// Gets the number of connections (links to other nodes).
        /// </summary>
        public int ConnectionsCount => m_nextNodes.Count;
        public int MaxConnection => m_maxConnection;
        public string Name => m_name;

        public Node[] GetAllNodes() => m_nextNodes.ToArray();

        /// <summary>
        /// Gets the connected node by its connection index.
        /// </summary>
        /// <param name="connectionIndex">The index of the connection.</param>
        /// <returns>The connected node or null if the index is out of range.</returns>
        public Node GetConnectedNodeByIndex(int connectionIndex)
        {
            // Check if the connectionIndex is within a valid range
            if (connectionIndex >= 0 && connectionIndex < m_nextNodes.Count)
            {
                // Return the connected node at the specified index
                return m_nextNodes[connectionIndex];
            }
            else
            {
                // Log an error if the index is out of range
                Debug.LogError($"Invalid connectionIndex: {connectionIndex}");
                return null;
            }
        }

        public static Port Create(string name, int maxConnection)
        {
            Port port = new Port();
            port.m_name = name;
            port.m_maxConnection = maxConnection;
            return port;
        }

        //internal void Restore(NodesContainer container)
        //{
        //    Debug.Log($"к востнавлению:{m_nextNodesID.Length}");
        //    if (m_nextNodesID == null)
        //    {
        //        Debug.LogError($"[{m_name}]m_nextNodesID == null");
        //    }
        //    if(m_nextNodesID.Length == 0) 
        //    {
        //        Debug.LogError($"[{m_name}]m_nextNodesID.Length == 0");
        //    }
        //    if (m_nextNodesID != null)
        //    {
        //        foreach (int id in m_nextNodesID)
        //        {
        //            // Attempt to find the Node in the provided NodesContainer using the ID
        //            Node find = container.GetNodeByID(id);

        //            if (find != null)
        //            { m_nextNodes.Add(find); }
        //            else
        //                Debug.LogError($"Failed to restore connection with Node. ID:{id}");
        //        }
        //    }
        //    Debug.Log($"востновлено:{m_nextNodes.Count}");
        //}

        ///// <summary>
        ///// Prepares the IDs of connected nodes before serialization.
        ///// </summary>
        //internal void SaveConnectedNodeIDs()
        //{
        //    m_nextNodesID = m_nextNodes.Select((n) => n.ID).Distinct().ToArray();
        //    //Debug.Log($"Сохронено:{m_nextNodesID.Length}");
        //}

        /// <summary>
        /// Add a link to the specified node, removing the oldest link if the maximum connection count is reached.
        /// </summary>
        /// <param name="node">The node to link to.</param>
        public void AddLink(Node node)
        {
            if (m_maxConnection == m_nextNodes.Count) { m_nextNodes.RemoveAt(0); }
            m_nextNodes.Add(node);
        }

        /// <summary>
        /// Remove a link based on the node's ID.
        /// </summary>
        /// <param name="nodeID">The ID of the node to unlink.</param>
        public void RemoveLinkById(int nodeID)
        {
            m_nextNodes.RemoveAll((n) => n.ID == nodeID);
        }

        public void RemoveLinkByIndex(int index)
        {
            m_nextNodes.RemoveAt(index);
        }

        internal void SetMaxConnectionsCount(int count)
        {
            if (count < 1)
            {
                Debug.LogError("Invalid number of connections");
                return;
            }

            m_maxConnection = count;

            while (m_nextNodes.Count > count) { m_nextNodes.RemoveAt(0); }
        }
    } 
}
