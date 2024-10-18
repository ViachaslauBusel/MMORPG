using Network.Core;
using Network.Object.Dynamic;
using Protocol;
using Protocol.MSG.Game;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Network.Replication
{
    public class ReplicationService : MonoBehaviour
    {
        private NetworkManager _networkManager;
        private DiContainer _container;
        private Dictionary<int, NetworkComponentsProvider> _objects = new Dictionary<int, NetworkComponentsProvider>();

        public event Action<GameObject> OnNetworkObjectSpawned;
        public event Action<GameObject> OnNetworkObjectDestroyed;

        [Inject]
        private void Construct(NetworkManager networkManager, DiContainer container)
        {
            _networkManager = networkManager;
            _container = container;

            _networkManager.RegisterHandler(Opcode.MSG_GAMEOBJECT_UPDATE, GameobjectUpdate);
            _networkManager.RegisterHandler(Opcode.MSG_GAMEOBJECT_DESTROY, GameobjectDestroy);
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
            if (_objects.ContainsKey(objectID) == false)
            {
                Debug.LogError($"[{objectID}]Error destroy game object, specified ID not found");
                return;
            }
            Debug.Log($"Destroy network object:{objectID}");
            _objects[objectID].NotifyComponentsPreDestroy();
            OnNetworkObjectDestroyed?.Invoke(_objects[objectID].gameObject);
            Destroy(_objects[objectID].gameObject);
            _objects.Remove(objectID);
        }

        private NetworkComponentsProvider GetObject(int id, out bool isNewObject)
        {
            isNewObject = _objects.ContainsKey(id) == false;

            if (isNewObject)
            {
                GameObject obj = _container.CreateEmptyGameObject($"network object {id}");


                var componentProvider = _container.InstantiateComponent<NetworkComponentsProvider>(obj);
                componentProvider.Init(id);
                _container.InstantiateComponent<DynamicObject>(obj);

                _objects.Add(id, componentProvider);
            }

            return _objects[id];
        }

        public void Clear()
        {
            foreach (var obj in _objects.Keys.ToArray())
            {
                DestroyNetworkObject(obj);
            }
        }

        private void OnDestroy()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_GAMEOBJECT_UPDATE);
            _networkManager.UnregisterHandler(Opcode.MSG_GAMEOBJECT_DESTROY);
        }
    }
}