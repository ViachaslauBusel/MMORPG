using NodeEditor.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NodeEditor.Data
{
    /// <summary>
    /// Manages output ports for connecting nodes.
    /// </summary>
    [System.Serializable]
    public class NodeOutputManager
    {
        [SerializeField]
        private List<Port> m_outputPorts = new List<Port>();

        ///// <summary>
        ///// Number of output ports
        ///// </summary>
        public int PortsCount => m_outputPorts.Count;

        /// <summary>
        /// Get port by index
        /// </summary>
        public virtual Port GetPortByIndex(int portIndex) => m_outputPorts[portIndex];

        /// <summary>
        /// Get port by name.
        /// </summary>
        public Port GetPortByName(string name) => m_outputPorts.Find((p) => p.Name == name);

        /// <summary>
        /// Add a new output port.
        /// </summary>
        public Port AddOutputPort(string name, int count)
        {
            Port port = Port.Create(name, count);
            m_outputPorts.Add(port);
            return port;
        }

        internal void RemovePortAt(int index)
        {
            m_outputPorts.RemoveAt(index);
        }

        ///// <summary>
        ///// Restores connections with nodes after deserialization, restoring node references based on the saved node IDs.
        ///// </summary>
        ///// <param name="nodesContainer">The container of nodes to restore connections.</param>
        //internal void RestoreConnections(NodesContainer nodesContainer)
        //{
        //    foreach (Port port in m_outputPorts) { port.Restore(nodesContainer); }
        //}

        ///// <summary>
        ///// Prepares the IDs of connected nodes before serialization for all ports.
        ///// </summary>
        //internal void PrepareConnectedNodeIDsForSerialization()
        //{
        //    foreach(Port port in m_outputPorts) { port.SaveConnectedNodeIDs(); }
        //} 

        /// <summary>
        /// Clears connections to the specified node from all output ports.
        /// </summary>
        /// <param name="nodeID">The ID of the node to remove connections to.</param>
        internal void ClearConnectionsToNode(int nodeID)
        {
            foreach (Port port in m_outputPorts)
            {
                port.RemoveLinkById(nodeID);
            }
        }

       
    }
}
