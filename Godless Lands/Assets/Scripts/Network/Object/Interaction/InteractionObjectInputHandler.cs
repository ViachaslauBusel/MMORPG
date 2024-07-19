using Network.Core;
using System;
using Zenject;

namespace Network.Object.Interaction
{

    public class InteractionObjectInputHandler
    {
        private InputManager _inputManager;
        private long _nextInteractionTime;

        [Inject]
        public InteractionObjectInputHandler(InputManager inputManager, NetworkManager networkManager)
        {
            _inputManager = inputManager;

            _inputManager.Enable();
        }

        internal void HandleInput(IInteractableObject interactionObject)
        {
            if (interactionObject == null) return;

            if (_inputManager.Keyboard.PressF.IsPressed())
            {
                if (_nextInteractionTime > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()) return;
                _nextInteractionTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 500;

                interactionObject.HandleInteraction();
            }
        }
    }
}
