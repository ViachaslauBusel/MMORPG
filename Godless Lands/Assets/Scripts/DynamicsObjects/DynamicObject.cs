using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DynamicsObjects
{
    internal class DynamicObject : MonoBehaviour
    {
        private NetworkComponentsProvider _networkComponentsProvider;

        public int ID => _networkComponentsProvider.NetworkGameObjectID;

        private void Awake()
        {
            _networkComponentsProvider = GetComponentInParent<NetworkComponentsProvider>();

            if(_networkComponentsProvider == null)
            {
               Debug.LogError("NetworkComponentsProvider not found");
            }
        }


    }
}
