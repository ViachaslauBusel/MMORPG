using Protocol;
using Protocol.MSG.Game;
using RUCP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Replication
{
    public class ReplicationService : MonoBehaviour
    {
        private NetworkManager m_networkManager;
        private DiContainer m_container;
        private Dictionary<int, NetworkComponentsProvider> m_objects = new Dictionary<int, NetworkComponentsProvider>();


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
            NetworkComponentsProvider componentProvider = GetObject(gameobject_update.GameobjectID);
            foreach (var updatedComponent in gameobject_update.UpdatedComponents) { componentProvider.UpdateComponent(updatedComponent); }
            foreach (var removaedComponent in gameobject_update.RemovedComponents) { componentProvider.RemoveComponent(removaedComponent); }
        }
        private void GameobjectDestroy(Packet packet)
        {
            packet.Read(out MSG_GAMEOBJECT_DESTROY_SC gameobject_destroy);
            foreach (int objectID in gameobject_destroy.DestroyedObjects)
            {
                if (!m_objects.ContainsKey(objectID))
                {
                    Debug.LogError($"[{objectID}]Error destroy game object, specified ID not found");
                    continue;
                }
                Destroy(m_objects[objectID]);
                m_objects.Remove(objectID);
            }
        }

        private NetworkComponentsProvider GetObject(int id)
        {

            if (!m_objects.ContainsKey(id))
            {
                GameObject obj = m_container.CreateEmptyGameObject($"network object {id}");

                var component = m_container.InstantiateComponent<NetworkComponentsProvider>(obj);
                component.Init(id);

                m_objects.Add(id, component);
            }
            return m_objects[id];
        }

        private void OnDestroy()
        {
            m_networkManager.UnregisterHandler(Opcode.MSG_GAMEOBJECT_UPDATE);
            m_networkManager.UnregisterHandler(Opcode.MSG_GAMEOBJECT_DESTROY);
        }
    }
}