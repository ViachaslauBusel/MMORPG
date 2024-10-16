using Protocol.Data.Replicated;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Network.Replication
{
    public class NetworkComponentsProvider : MonoBehaviour
    {
        private int _networkGameObjectID;
        private Dictionary<int, INetworkDataHandler> m_components = new Dictionary<int, INetworkDataHandler>();
        private DataHandlerStorage m_dataHandlerStorage;
        
        public int NetworkGameObjectID => _networkGameObjectID;

        [Inject]
        private void Construct(DataHandlerStorage dataHandlerStorage)
        {
            m_dataHandlerStorage = dataHandlerStorage;
        }
        internal void Init(int id)
        {
            if(NetworkGameObjectID != 0)
            {
                Debug.LogError($"Attempt to reinitialize a network object:{NetworkGameObjectID} -> {id}");
            }
            _networkGameObjectID = id;
        }
       
        public void CheckOrCreateComponent(IReplicationData updatedData)
        {
            int dataID = updatedData.GetID();
            if (!m_components.ContainsKey(dataID))
            {
                INetworkDataHandler dataHandler = m_dataHandlerStorage.CreateDataHandler(gameObject, updatedData);
                if (dataHandler == null) return;
                m_components.Add(dataID, dataHandler);
            }
        }

        internal void UpdateComponent(IReplicationData updatedData)
        {
            int dataID = updatedData.GetID();

            if(m_components.ContainsKey(dataID))
            {
                m_components[dataID].UpdateData(updatedData);
            }
        }

        internal void RemoveComponent(int removaedComponent)
        {
            if (!m_components.ContainsKey(removaedComponent))
            {
                Debug.LogError($"[{removaedComponent}]Failed to remove component because it was not found");
                return;
            }

            (m_components[removaedComponent] as IDestroyNotifier)?.NotifyPreDestroy();
            Destroy(m_components[removaedComponent] as MonoBehaviour);
            m_components.Remove(removaedComponent);
        }
        internal void NotifyComponentsPreDestroy()
        {
            foreach (var component in m_components.Values)
            {
                (component as IDestroyNotifier)?.NotifyPreDestroy();
            }
        }
    }
}