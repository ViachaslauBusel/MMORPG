using NetworkObjectVisualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerCharacterVisualEventNotifier : IDisposable
    {
        private PlayerCharacterNetworkObjecEventNotifier _networkEventNotifier;
        private IVisualRepresentation _playerCharacterVisualRepresentation;
        private GameObject _playerCharacterVisualObject;

        public event Action<GameObject> OnPlayerCharacterVisualSpawned;
        public event Action<GameObject> OnPlayerCharacterVisualDestroyed;

        [Inject]
        public void Construct(PlayerCharacterNetworkObjecEventNotifier networkEventNotifier)
        {
            _networkEventNotifier = networkEventNotifier;

            _networkEventNotifier.OnPlayerCharacterObjectSpawned += OnPlayerCharacterObjectSpawned;
            _networkEventNotifier.OnPlayerCharacterObjectDestroyed += OnPlayerCharacterObjectDestroyed;

        }

        private void OnPlayerCharacterObjectDestroyed(GameObject @object)
        {
            Debug.Log("PlayerCharacterVisualEventNotifier: OnPlayerCharacterObjectDestroyed");
            OnVisualObjectUpdated(null);

            if(_playerCharacterVisualRepresentation != null)
            {
                _playerCharacterVisualRepresentation.OnVisualObjectUpdated -= OnVisualObjectUpdated;
            }
        }

        private void OnPlayerCharacterObjectSpawned(GameObject @object)
        {
            Debug.Log("PlayerCharacterVisualEventNotifier: OnPlayerCharacterObjectSpawned");
            if(_playerCharacterVisualRepresentation != null)
            {
                _playerCharacterVisualRepresentation.OnVisualObjectUpdated -= OnVisualObjectUpdated;
            }

            _playerCharacterVisualRepresentation = @object.GetComponent<IVisualRepresentation>();
            if (_playerCharacterVisualRepresentation == null) return;

            OnVisualObjectUpdated(_playerCharacterVisualRepresentation.VisualObject);
            _playerCharacterVisualRepresentation.OnVisualObjectUpdated += OnVisualObjectUpdated;
        }

        private void OnVisualObjectUpdated(GameObject @object)
        {
            if(_playerCharacterVisualObject == @object)
            {
                return;
            }

            if(_playerCharacterVisualObject != null)
            {
                OnPlayerCharacterVisualDestroyed?.Invoke(_playerCharacterVisualObject);
            }

            _playerCharacterVisualObject = @object;
            OnPlayerCharacterVisualSpawned?.Invoke(_playerCharacterVisualObject);
        }

        public void Dispose()
        {
            _networkEventNotifier.OnPlayerCharacterObjectSpawned -= OnPlayerCharacterObjectSpawned;
            _networkEventNotifier.OnPlayerCharacterObjectDestroyed -= OnPlayerCharacterObjectDestroyed;
        }
    }
}
