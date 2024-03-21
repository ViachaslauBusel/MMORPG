using Assets.Scripts.Services.Replication;
using Services.Replication;
using System;
using UnitVisualCache;
using UnityEngine;
using Zenject;

namespace NetworkObjectVisualization
{
    public abstract class NetworkObjectVisualHandler : MonoBehaviour, IVisualRepresentation, IDestroyNotifier
    {
        private GameObject _visualObject;
        private UnitVisualCacheService _visualCacheService;
        private NetworkComponentsProvider _networkComponentsProvider;
        protected bool _isNeedChaceVisual;

        public event Action<GameObject> OnVisualObjectUpdated;
        public GameObject VisualObject => _visualObject;

        [Inject]
        private void Construct(UnitVisualCacheService visualCacheService)
        {
            _visualCacheService = visualCacheService;
        }

        protected void Awake()
        {
            _networkComponentsProvider = GetComponent<NetworkComponentsProvider>();
        }

        private void CacheVisual()
        {
            if (_visualObject == null) return;

            DetachVisualObjectScript();

            // Caching the visual object for later use as a corpse
            _visualCacheService.Add(_networkComponentsProvider.NetworkGameObjectID, _visualObject);
            _visualObject = null;
            OnVisualObjectUpdated?.Invoke(null);
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

        private void DetachVisualObjectScript()
        {
            if (_visualObject == null) return;

            var visualObjectScript = _visualObject.GetComponentsInChildren<IVisualObjectScript>();

            foreach (var script in visualObjectScript)
            {
                script.DetachFromNetworkObject();
            }
        }

        protected void SetVisualObject(GameObject visualObject)
        {
            _visualObject = visualObject;

            var visualObjectScript = _visualObject.GetComponentsInChildren<IVisualObjectScript>();

            foreach (var script in visualObjectScript)
            {
                script.AttachToNetworkObject(gameObject);
            }

            OnVisualObjectUpdated?.Invoke(_visualObject);
        }

        /// <summary>
        ///  Attempt to get the visual object from the cache
        /// </summary>
        /// <param name="visualObjectId"></param>
        /// <returns></returns>
        protected GameObject GetCachedVisualObject(int visualObjectId)
        {
            if (visualObjectId != 0)
            {
                return _visualCacheService.Get(visualObjectId, transform);
            }

            return null;
        }

        // This method is called just before the object is destroyed
        public void NotifyPreDestroy()
        {
            if(_isNeedChaceVisual) CacheVisual();
            else DestroyExistingUnitObject();
        }
    }
}
