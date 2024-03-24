using DynamicsObjects;
using Protocol;
using Protocol.MSG.Game;
using RUCP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Services.Replication
{
    public class ReplicationService : MonoBehaviour
    {
        private NetworkManager m_networkManager;
        private DiContainer m_container;
        private Dictionary<int, NetworkComponentsProvider> m_objects = new Dictionary<int, NetworkComponentsProvider>();

        public event Action<GameObject> OnNetworkObjectSpawned;
        public event Action<GameObject> OnNetworkObjectDestroyed;

        [Inject]
        private void Construct(NetworkManager networkManager, DiContainer container)
        {
            m_networkManager = networkManager;
            m_container = container;

            m_networkManager.RegisterHandler(Opcode.MSG_GAMEOBJECT_UPDATE, GameobjectUpdate);
            m_networkManager.RegisterHandler(Opcode.MSG_GAMEOBJECT_DESTROY, GameobjectDestroy);
        }

        private void GameobjectUpdate(Packet packet)
        {
            packet.Read(out MSG_GAMEOBJECT_UPDATE_SC gameobject_update);
           // Debug.Log($"network object updated:{gameobject_update.GameobjectID}");
            NetworkComponentsProvider componentProvider = GetObject(gameobject_update.GameobjectID, out bool isNewObject);
            foreach (var updatedComponent in gameobject_update.UpdatedComponents) { componentProvider.CheckOrCreateComponent(updatedComponent); }
            foreach (var updatedComponent in gameobject_update.UpdatedComponents) { componentProvider.UpdateComponent(updatedComponent); }
            foreach (var removaedComponent in gameobject_update.RemovedComponents) { componentProvider.RemoveComponent(removaedComponent); }

            if (isNewObject) OnNetworkObjectSpawned?.Invoke(componentProvider.gameObject);
        }

        private void GameobjectDestroy(Packet packet)
        {
            packet.Read(out MSG_GAMEOBJECT_DESTROY_SC gameobject_destroy);
            foreach (int objectID in gameobject_destroy.DestroyedObjects)
            {
                DestroyNetworkObject(objectID);
            }
        }

        private void DestroyNetworkObject(int objectID)
        {
            if (m_objects.ContainsKey(objectID) == false)
            {
                Debug.LogError($"[{objectID}]Error destroy game object, specified ID not found");
                return;
            }
            m_objects[objectID].NotifyComponentsPreDestroy();
            OnNetworkObjectDestroyed?.Invoke(m_objects[objectID].gameObject);
            Destroy(m_objects[objectID].gameObject);
            m_objects.Remove(objectID);
        }

        private NetworkComponentsProvider GetObject(int id, out bool isNewObject)
        {
            isNewObject = m_objects.ContainsKey(id) == false;

            if (isNewObject)
            {
                GameObject obj = m_container.CreateEmptyGameObject($"network object {id}");

                
                var componentProvider = m_container.InstantiateComponent<NetworkComponentsProvider>(obj);
                componentProvider.Init(id);
                m_container.InstantiateComponent<DynamicObject>(obj);

                m_objects.Add(id, componentProvider);
            }

            return m_objects[id];
        }

        public void Clear()
        {
            foreach (var obj in m_objects.Keys.ToArray())
            {
                DestroyNetworkObject(obj);
            }
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_GAMEOBJECT_UPDATE);
            m_networkManager.UnregisterHandler(Opcode.MSG_GAMEOBJECT_DESTROY);
        }
    }
}