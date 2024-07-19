using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Network.Object.Visualization
{

    /// <summary>
    /// This interface is used on scripts for visual objects that can subscribe to the events of the network object to which they are attached
    /// </summary>
    public interface IVisualObjectScript
    {
        /// <summary>
        /// This method is called when the visual object is attached to a network object.
        /// </summary>
        /// <param name="networkObjectOwner">The network object to which the visual object is attached.</param>
        void AttachToNetworkObject(GameObject networkObjectOwner);

        /// <summary>
        /// This method is called when the visual object is detached from a network object.
        /// </summary>
        void DetachFromNetworkObject();
    }
}
