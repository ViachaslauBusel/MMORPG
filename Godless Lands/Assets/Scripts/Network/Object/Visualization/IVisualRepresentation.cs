using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Network.Object.Visualization
{
    public interface IVisualRepresentation
    {
        GameObject VisualObject { get; }

        event Action<GameObject> OnVisualObjectUpdated;
    }
}
