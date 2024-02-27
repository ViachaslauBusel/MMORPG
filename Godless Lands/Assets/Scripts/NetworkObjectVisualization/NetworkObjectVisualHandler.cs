using Protocol.Data.Replicated.Skins;
using Protocol.Data.Replicated;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitVisualCache;
using UnityEngine;
using Walkers.Monsters;
using Zenject;
using Assets.Scripts.Services.Replication;

namespace NetworkObjectVisualization
{
    public abstract class NetworkObjectVisualHandler : MonoBehaviour, IVisualRepresentation, IDestroyNotifier
    {
        private GameObject _unitObject;
        private UnitVisualCacheService _visualCacheService;
        private NetworkComponentsProvider _networkComponentsProvider;

        public event Action<GameObject> OnVisualObjectUpdated;

        public GameObject VisualObject => _unitObject;

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
            if (_unitObject == null) return;

            _visualCacheService.Add(_networkComponentsProvider.NetworkGameObjectID, _unitObject);
            _unitObject = null;
            OnVisualObjectUpdated?.Invoke(null);
        }

        protected void UpdateVisualObject (int visualObjectId)
        {
            DestroyExistingUnitObject();

            _unitObject = GetCachedVisualObject(visualObjectId) ?? CreateNewUnit();

            var visualObjectScript = _unitObject.GetComponentsInChildren<IVisualObjectScript>();

            foreach (var script in visualObjectScript)
            {
                script.SubscribeToNetworkObject(gameObject);
            }

            OnVisualObjectUpdated?.Invoke(_unitObject);
        }

        private void DestroyExistingUnitObject()
        {
            if (_unitObject != null)
            {
                Destroy(_unitObject);
            }
        }

        private GameObject GetCachedVisualObject(int visualObjectId)
        {
            if (visualObjectId != 0)
            {
                return _visualCacheService.Get(visualObjectId, transform);
            }

            return null;
        }

        protected abstract GameObject CreateNewUnit();

        public void NotifyPreDestroy()
        {
            if (true)
            {
                CacheVisual();
                return;
            }
        }
    }
}
