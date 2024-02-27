using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GameWorldInteractions
{
    public class InteractionTrigger : MonoBehaviour
    {
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private InteractionIndicator _interactionIndicator;
        private PlayerCharacterVisualEventNotifier _playerCharacterEvent;
        private InputManager _inputManager;
        private GameObject _playerCharacterVisual;

        [Inject]
        private void Construct(InteractableObjectsRegistry interactableObjectsRegistry, InteractionIndicator interactionIndicator, InputManager inputManager, PlayerCharacterVisualEventNotifier playerCharacterEvent)
        {
            _interactableObjectsRegistry = interactableObjectsRegistry;
            _interactionIndicator = interactionIndicator;
            _inputManager = inputManager;
            _playerCharacterEvent = playerCharacterEvent;

            _playerCharacterEvent.OnPlayerCharacterVisualSpawned += HandlePlayerCharacterSpawned;
            _playerCharacterEvent.OnPlayerCharacterVisualDestroyed += HandlePlayerCharacterDestroyed;
        }

        private void HandlePlayerCharacterDestroyed(GameObject @object)
        {
            _playerCharacterVisual = null;
        }

        private void HandlePlayerCharacterSpawned(GameObject @object)
        {
           _playerCharacterVisual = @object;
        }

        public void Update()
        {
          if (_playerCharacterVisual == null) return;

            GameObject interactableObject = _interactableObjectsRegistry.GetClosestInteractableObject(_playerCharacterVisual.transform.position, 2f);

            // Make indicator visible if there is an interactable object in range.
            _interactionIndicator.SetVisible(interactableObject != null);
        }
    }
}
