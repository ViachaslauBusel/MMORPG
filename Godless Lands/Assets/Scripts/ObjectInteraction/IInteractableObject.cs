using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectInteraction
{
    public interface IInteractableObject
    {
        public int NetworkGameObjectID { get; }
        public Vector3 Position { get;  }

        void HandleInteraction();
    }
}
