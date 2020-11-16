using RUCP;
using RUCP.Handler;
using RUCP.Packets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Resource
{
    public class ResourcesManager : MonoBehaviour, Manager
    {
        public ResourceList resourceList;
        private static Dictionary<int, GhostResource> resources;



        private void Awake()
        {
            HandlersStorage.RegisterHandler(Types.ResourceCreate, ResourceCreate);
            HandlersStorage.RegisterHandler(Types.ResourceDelete, ResourceDelete);
        }



        private void Start()
        {
          
            resources = new Dictionary<int, GhostResource>();
        }


        public void AllDestroy()
        {
            foreach (GhostResource resource in resources.Values)
                Destroy(resource.gameObject);
            resources.Clear();
          
        }


        private void ResourceDelete(Packet nw)
        {
            int id = nw.ReadInt();
            if (resources.ContainsKey(id))
            {
                resources[id].DestroyAnim();
                resources.Remove(id);
            }
        }

        private void ResourceCreate(Packet nw)
        {
            int id = nw.ReadInt();
            int idSkin = nw.ReadInt();
            Vector3 postion = nw.ReadVector3();
            float rotation = nw.ReadFloat();
            if (resources.ContainsKey(id)) { print("error create resource"); return; } //Если монстр с таким ид уже создан


            GameObject prefabResource = resourceList.GetPrefab(idSkin);
            if (prefabResource == null) { print("Error resource skin: " + idSkin); return; }

            GameObject _obj = Instantiate(prefabResource, postion, Quaternion.Euler(0.0f, rotation, 0.0f));
            _obj.transform.SetParent(transform);

            GhostResource _resource = _obj.GetComponent<GhostResource>();
            _resource.ID = id;
            _resource.SetName(resourceList.GetFabric(idSkin).name);

            resources.Add(_resource.ID, _resource);
        }


        private void OnDestroy()
        {
            HandlersStorage.UnregisterHandler(Types.ResourceCreate);
            HandlersStorage.UnregisterHandler(Types.ResourceDelete);
        }
    }
}