using Network.Replication;
using UnityEngine;

namespace Network.Object.Dynamic
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
