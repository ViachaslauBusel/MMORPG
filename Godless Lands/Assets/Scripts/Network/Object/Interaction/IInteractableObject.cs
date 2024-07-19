using UnityEngine;

namespace Network.Object.Interaction
{
    public interface IInteractableObject
    {
        public int NetworkGameObjectID { get; }
        public Vector3 Position { get;  }

        void HandleInteraction();
    }
}
