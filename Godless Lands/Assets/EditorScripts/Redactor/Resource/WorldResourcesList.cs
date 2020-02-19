using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Resource
{
    public class WorldResourcesList : ScriptableObject
    {
        public List<WorldFabric> worldResources;

        public void Add(WorldFabric resource)
        {
            if (worldResources == null) { worldResources = new List<WorldFabric>(); }

            worldResources.Add(resource);
        }

        public void Remove(WorldFabric resource)
        {
            if (resource == null) return;
            worldResources.Remove(resource);
        }
    }
}