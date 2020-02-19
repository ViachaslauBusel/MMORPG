#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCRedactor
{
    [System.Serializable]
    public class WorldNPC
    {
        public int id;
        public Vector3 point;
        public float radius;

        public WorldNPC(int id, Vector3 point, float radius)
        {
            this.id = id;
            this.point = point;
            this.radius = radius;
        }
    }
}
#endif
