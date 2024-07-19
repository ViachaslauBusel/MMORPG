using Network.Replication;
using System;
using UnityEngine;

namespace Network.Object.Visualization
{
    public abstract class NetworkObjectVisualHandler : MonoBehaviour, IVisualRepresentation, IDestroyNotifier
    {
        protected GameObject _visualObject;
        protected NetworkComponentsProvider _networkComponentsProvider;

        public event Action<GameObject> OnVisualObjectUpdated;
        public GameObject VisualObject => _visualObject;


        protected void Awake()
        {
            _networkComponentsProvider = GetComponent<NetworkComponentsProvider>();
        }

        protected void DestroyExistingUnitObject()
        {
            if (_visualObject != null)
            {
                DetachVisualObjectScript();

                Destroy(_visualObject);
                _visualObject = null;
            }
        }

        protected void DetachVisualObjectScript()
        {
            if (_visualObject == null) return;

            Debug.Log($"DetachVisualObjectScript:{_visualObject.name}");
            var visualObjectScript = _visualObject.GetComponentsInChildren<IVisualObjectScript>();

            foreach (var script in visualObjectScript)
            {
                script.DetachFromNetworkObject();
            }
        }

        protected void SetVisualObject(GameObject visualObject)
        {
            bool isUpdated = _visualObject != visualObject;
            _visualObject = visualObject;

            if (_visualObject != null)
            {
                var visualObjectScript = _visualObject.GetComponentsInChildren<IVisualObjectScript>();

                foreach (var script in visualObjectScript)
                {
                    script.AttachToNetworkObject(gameObject);
                }
            }

            if (isUpdated) OnVisualObjectUpdated?.Invoke(_visualObject);
        }

        // This method is called just before the object is destroyed
        public virtual void NotifyPreDestroy()
        {
             DestroyExistingUnitObject();
        }
    }
}
