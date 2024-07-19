using Network.Object.Interaction.UI;
using Player;
using UnityEngine;
using Zenject;

namespace Network.Object.Interaction
{
    /// <summary>
    /// Activates the interaction indicator when the player is close to an interactable object.
    /// </summary>
    public class InteractionTrigger : MonoBehaviour
    {
        private InteractableObjectsRegistry _interactableObjectsRegistry;
        private InteractionIndicator _interactionIndicator;
        private PlayerCharacterVisualEventNotifier _playerCharacterEvent;
        private InteractionObjectInputHandler _inputHandler;
        private GameObject _playerCharacterVisual;

        [Inject]
        private void Construct(InteractableObjectsRegistry interactableObjectsRegistry, InteractionIndicator interactionIndicator,
                               InteractionObjectInputHandler inputHandler, PlayerCharacterVisualEventNotifier playerCharacterEvent)
        {
            _interactableObjectsRegistry = interactableObjectsRegistry;
            _interactionIndicator = interactionIndicator;
            _inputHandler = inputHandler;
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

            var interactableObject = _interactableObjectsRegistry.GetClosestInteractableObject(_playerCharacterVisual.transform.position, 2f);

            // Make indicator visible if there is an interactable object in range.
            _interactionIndicator.SetVisible(interactableObject != null);

            // Handle input
            _inputHandler.HandleInput(interactableObject);
        }
    }
}
