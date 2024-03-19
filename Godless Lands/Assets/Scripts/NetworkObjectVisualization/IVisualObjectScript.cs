using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NetworkObjectVisualization
{

    /// <summary>
    /// This interface is used so that visual objects can subscribe to the events of the network object to which they are attached
    /// </summary>
    public interface IVisualObjectScript
    {
        void SubscribeToNetworkObject(GameObject networkObjectOwner);
        void UnsubscribeFromNetworkObject();
    }
}
