using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resource
{
    [System.Serializable]
    public class WorldFabric
    {
        public int id;
        public Vector3 point;
        public float radius;
        public WorldFabric(int id, Vector3 point, float radius)
        {
            this.id = id;
            this.point = point;
            this.radius = radius;
        }
    }
}