using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resource
{
    public class ResourceList : ObjectList
    {
        public List<Fabric> resources;

        public override void Add(object obj)
        {
            Fabric res = obj as Fabric;
            if (res == null) return;
            if (resources == null) resources = new List<Fabric>();
            if (res.id == 0) res.id++;

            while (ConstainsKey(res.id)) res.id++;
            resources.Add(res);
        }

        private bool ConstainsKey(int id)
        {
            foreach (Fabric res in resources)
            {
                if (res.id == id) return true;
            }
            return false;
        }

        public override int Count
        {
            get {if (resources == null) return 0;
                return resources.Count; }
        }

        public override System.Object this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= resources.Count) return null;
                return resources[index];
            }
        }

        public GameObject GetPrefab(int id)
        {
            foreach (Fabric res in resources)
            {
                if (res.id == id) return res.prefab;
            }
            return null;
        }
        public Fabric GetFabric(int id)
        {
            foreach (Fabric res in resources)
            {
                if (res.id == id) return res;
            }
            return null;
        }

        public override void Remove(object obj)
        {
            Fabric fabric = obj as Fabric;
            if (fabric == null) return;
            resources.Remove(fabric);
        }
    }
}