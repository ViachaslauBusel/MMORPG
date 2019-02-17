using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resource
{
    [System.Serializable]
    public class Fabric
    {
        public int id;
        public GameObject prefab;
        public string name;
        public float startSpawn;
        public float timeSpawn;
        public List<Drop> drops;

        public Fabric()
        {
            drops = new List<Drop>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return id == (obj as Fabric).id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}