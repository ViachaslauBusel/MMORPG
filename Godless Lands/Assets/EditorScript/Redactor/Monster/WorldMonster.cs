using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterRedactor
{
    [System.Serializable]
    public class WorldMonster
    {
        public int id; 
        public Vector3 point;
        public float radius;

        public WorldMonster(int id, Vector3 point, float radius)
        {
            this.id = id;
            this.point = point;
            this.radius = radius;
        }
    }
}