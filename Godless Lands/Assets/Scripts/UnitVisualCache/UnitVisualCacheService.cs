using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UnitVisualCache
{
    public class UnitVisualCacheService : ITickable
    {
        class VisualCacheEntry
        {
            public readonly GameObject Visual;
            public readonly long TimeStamp;

            public VisualCacheEntry(GameObject visual)
            {
                Visual = visual;
                TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
        }
        private Dictionary<int, VisualCacheEntry> _cache = new Dictionary<int, VisualCacheEntry>();
        private List<int> _needRemoveVisual = new List<int>();
        private GameObject _visualChaceHolder;

        public UnitVisualCacheService()
        {
            _visualChaceHolder = new GameObject("VisualChaceHolder");
        }

        public void Add(int id, GameObject visualObject)
        {
            Debug.Log($"Add visual for unit with id {id} to cache");
            if (_cache.TryGetValue(id, out var existingVisual))
            {
                Debug.LogWarning($"Visual for unit with id {id} already exists in cache");
                GameObject.Destroy(existingVisual.Visual);
            }

            visualObject.transform.SetParent(_visualChaceHolder.transform);
            _cache[id] = new VisualCacheEntry(visualObject); // Use indexer to add or update the value
        }

        public GameObject Get(int id, Transform transform)
        {
            if (_cache.TryGetValue(id, out var visual))
            {
                Debug.Log($"Get visual for unit with id {id} from cache");
                _cache.Remove(id);
                visual.Visual.transform.SetParent(transform);
                return visual.Visual;
            }
            Debug.LogWarning($"Visual for unit with id {id} not found in cache");
            return null;
        }

        public void Tick()
        {
            foreach (var entry in _cache)
            {
                if ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - entry.Value.TimeStamp) > 3_000)
                {
                    _needRemoveVisual.Add(entry.Key);
                }
            }
            foreach (var id in _needRemoveVisual)
            {
                Debug.Log($"Remove visual for unit with id {id} from cache");
                GameObject.Destroy(_cache[id].Visual);
                _cache.Remove(id);
            }
            _needRemoveVisual.Clear();
        }
    }
}
