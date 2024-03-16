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
    /// <summary>
    /// This class is responsible for handling input from the player and sending it to the network manager.
    /// </summary>
    public class InteractionObjectInputHandler
    {
        private InputManager _inputManager;
        private NetworkManager _networkManager;
        private long _nextInteractionTime;

        [Inject]
        public InteractionObjectInputHandler(InputManager inputManager, NetworkManager networkManager)
        {
            _inputManager = inputManager;
            _networkManager = networkManager;

            _inputManager.Enable();
        }

        internal void HandleInput(int networkGameObjecttId)
        {
            if (networkGameObjecttId == 0) return;

            if (_inputManager.Keyboard.PressF.IsPressed())
            {
                if (_nextInteractionTime > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()) return;
                _nextInteractionTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + 500;

               MSG_OBJECT_INTERACTION_REQUEST_CS msg = new MSG_OBJECT_INTERACTION_REQUEST_CS();
                msg.ObjectId = networkGameObjecttId;
                _networkManager.Client.Send(msg);
            }
        }
    }
}
