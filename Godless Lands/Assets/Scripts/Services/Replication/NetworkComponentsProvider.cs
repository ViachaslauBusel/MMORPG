using Protocol.Data.Replicated;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Replication
{
    public class NetworkComponentsProvider : MonoBehaviour
    {
        private Dictionary<int, INetworkDataHandler> m_components = new Dictionary<int, INetworkDataHandler>();
        private DataHandlerStorage m_dataHandlerStorage;
        
        public int NetworkGameObjectID { get; private set; }

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
            NetworkGameObjectID = id;
        }
       

        internal void UpdateComponent(IReplicationData updatedData)
        {
            int dataID = updatedData.GetID();
            if (!m_components.ContainsKey(dataID))
            {
                INetworkDataHandler dataHandler = m_dataHandlerStorage.CreateDataHandler(gameObject, updatedData);
                if (dataHandler == null) return;
                m_components.Add(dataID, dataHandler);
            }

            m_components[dataID].UpdateData(updatedData);
        }

        internal void RemoveComponent(int removaedComponent)
        {
            if (!m_components.ContainsKey(removaedComponent))
            {
                Debug.LogError($"[{removaedComponent}]Failed to remove component because it was not found");
                return;
            }

            Destroy(m_components[removaedComponent] as MonoBehaviour);
            m_components.Remove(removaedComponent);
        }
    }
}