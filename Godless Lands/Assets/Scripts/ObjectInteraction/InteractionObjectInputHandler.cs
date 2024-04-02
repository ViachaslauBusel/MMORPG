using Protocol.MSG.Game.ObjectInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ObjectInteraction
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
