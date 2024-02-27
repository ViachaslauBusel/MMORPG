using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NetworkObjectVisualization
{
    public interface IVisualObjectScript
    {
        void SubscribeToNetworkObject(GameObject networkObjectOwner);
        void UnsubscribeFromNetworkObject();
    }
}
