using AssetPerformanceToolkit.AssetManagement;
using Network.Replication;
using System;
using UnityEngine;

namespace Network.Object.Visualization
{
    public abstract class NetworkObjectVisualHandler : MonoBehaviour, IVisualRepresentation, IDestroyNotifier
    {
        protected AssetInstance _visualObjectInstance;
        protected NetworkComponentsProvider _networkComponentsProvider;

        public event Action<GameObject> OnVisualObjectUpdated;
        public event Action OnVisualObjectDestroyed;
        public GameObject VisualObject => _visualObjectInstance?.InstanceObject;


        protected void Awake()
        {
            _networkComponentsProvider = GetComponent<NetworkComponentsProvider>();
        }

        protected void DestroyExistingUnitObject()
        {
            if (_visualObjectInstance != null)
            {
                DetachVisualObjectScript();

                _visualObjectInstance.Release();
                _visualObjectInstance = null;
                OnVisualObjectDestroyed?.Invoke();
            }
        }

        protected void DetachVisualObjectScript()
        {
            if (_visualObjectInstance == null) return;

            var visualObjectScript = _visualObjectInstance.InstanceObject.GetComponentsInChildren<IVisualObjectScript>();

            foreach (var script in visualObjectScript)
            {
                script.DetachFromNetworkObject();
            }
        }

        protected void SetVisualObject(AssetInstance visualObjectInstance)
        {
            bool isUpdated = _visualObjectInstance != visualObjectInstance;

            if(isUpdated == false) Debug.LogError("Visual object is already set");

            _visualObjectInstance = visualObjectInstance;
            GameObject visualObject = null;

            if (_visualObjectInstance != null)
            {
                visualObject = _visualObjectInstance.InstanceObject;
                var visualObjectScript = visualObject.GetComponentsInChildren<IVisualObjectScript>();

                foreach (var script in visualObjectScript)
                {
                    script.AttachToNetworkObject(gameObject);
                }
            }

            if (isUpdated) OnVisualObjectUpdated?.Invoke(visualObject);
        }

        // This method is called just before the object is destroyed
        public virtual void NotifyPreDestroy()
        {
             DestroyExistingUnitObject();
        }
    }
}
