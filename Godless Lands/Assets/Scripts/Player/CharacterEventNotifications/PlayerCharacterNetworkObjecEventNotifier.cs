using NetworkObjectVisualization;
using Services.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerCharacterNetworkObjecEventNotifier : IDisposable
    {
        private SessionManagmentService _sessionManagementService;
        private ReplicationService _replicationService;
        private int _playerCharacterObjectId;
        private bool _isPlayerCharacterSpawned;

        public event Action<GameObject> OnPlayerCharacterObjectSpawned;
        public event Action<GameObject> OnPlayerCharacterObjectDestroyed;

        [Inject]
        public void Construct(SessionManagmentService sessionManagementService, ReplicationService replicationService)
        {
            _sessionManagementService = sessionManagementService;
            _replicationService = replicationService;

            _sessionManagementService.OnCharacterObjectIDChanged += HandleCharacterObjectIdChanged;
            _replicationService.OnNetworkObjectSpawned += HandleNetworkObjectSpawned;
            _replicationService.OnNetworkObjectDestroyed += HandleNetworkObjectDestroyed;
        }

        private void HandleNetworkObjectDestroyed(GameObject networkObject)
        {
            if (_isPlayerCharacterSpawned == false) return;

            NetworkComponentsProvider networkComponentsProvider = networkObject.GetComponent<NetworkComponentsProvider>();
            if (networkComponentsProvider != null && networkComponentsProvider.NetworkGameObjectID == _playerCharacterObjectId)
            {
                _isPlayerCharacterSpawned = false;
                OnPlayerCharacterObjectDestroyed?.Invoke(networkObject);
            }
        }

        private void HandleNetworkObjectSpawned(GameObject networkObject)
        {
            NetworkComponentsProvider networkComponentsProvider = networkObject.GetComponent<NetworkComponentsProvider>();
            if(networkComponentsProvider == null || networkComponentsProvider.NetworkGameObjectID != _playerCharacterObjectId) return;

            if (_isPlayerCharacterSpawned)
            {
                Debug.LogError("Player character object is already spawned");
                return;
            }

            _isPlayerCharacterSpawned = true;
            OnPlayerCharacterObjectSpawned?.Invoke(networkObject);
        }

        private void HandleCharacterObjectIdChanged(int newObjectId)
        {
            _playerCharacterObjectId = newObjectId;
            if (_isPlayerCharacterSpawned)
            {
                Debug.LogError("Player character object ID changed while the character object is spawned");
            }
        }

        public void Dispose()
        {
            _sessionManagementService.OnCharacterObjectIDChanged -= HandleCharacterObjectIdChanged;
            _replicationService.OnNetworkObjectSpawned -= HandleNetworkObjectSpawned;
            _replicationService.OnNetworkObjectDestroyed -= HandleNetworkObjectDestroyed;
        }
    }
}
