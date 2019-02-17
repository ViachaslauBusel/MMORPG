using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Resource
{
    public class ResourcesManager : MonoBehaviour
    {
        public ResourceList resourceList;
        private static Dictionary<int, GhostResource> resources;



        private void Awake()
        {
            RegisteredTypes.RegisterTypes(Types.ResourceCreate, ResourceCreate);
            RegisteredTypes.RegisterTypes(Types.ResourceDelete, ResourceDelete);
        }



        private void Start()
        {
          
            resources = new Dictionary<int, GhostResource>();
        }

        private void ResourceDelete(NetworkWriter nw)
        {
            int id = nw.ReadInt();
            if (resources.ContainsKey(id))
            {
                resources[id].DestroyAnim();
                resources.Remove(id);
            }
        }

        private void ResourceCreate(NetworkWriter nw)
        {
            int id = nw.ReadInt();
            int idSkin = nw.ReadInt();
            Vector3 postion = nw.ReadVec3();
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
            RegisteredTypes.UnregisterTypes(Types.ResourceCreate);
            RegisteredTypes.UnregisterTypes(Types.ResourceDelete);
        }
    }
}