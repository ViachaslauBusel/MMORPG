using Network.Object.Visualization.VisualCache;
using UnityEngine;
using Zenject;

namespace Network.Object.Visualization
{
    internal abstract class CachedNetworkObjectVisualHandler : NetworkObjectVisualHandler
    {
        private UnitVisualCacheService _visualCacheService;

        protected abstract bool IsNeedChaceVisual { get; }

        [Inject]
        private void Construct(UnitVisualCacheService visualCacheService)
        {
            _visualCacheService = visualCacheService;
        }

        private void CacheVisual()
        {
            if (_visualObject == null) return;

            DetachVisualObjectScript();

            // Caching the visual object for later use as a corpse
            _visualCacheService.Add(_networkComponentsProvider.NetworkGameObjectID, _visualObject);
            SetVisualObject(null);
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
        public override void NotifyPreDestroy()
        {
            if (IsNeedChaceVisual) CacheVisual();
            else DestroyExistingUnitObject();
        }
    }
}
