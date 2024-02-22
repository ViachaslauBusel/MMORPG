﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Protocol.MSG.Game.ToServer;
using Protocol.MSG.Game.ToClient;
using Protocol.MSG.Game.PlayerDeadState;

namespace UI.PlayerCharacterDead
{
    public class PlayerCharacterDeadWindow : MonoBehaviour
    {
        private Canvas _canvas;
        private NetworkManager _networkManager;

        [Inject]
        private void Construct(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        public void Open()
        {
            _canvas.enabled = true;
        }

        public void Close() 
        {
            _canvas.enabled = false;
        }

        public void Respawn()
        {
            // Send message to server to revive the player character
            _networkManager.Client.Send(new MSG_PLAYER_DEATH_STATE_CS());
            Close();
        }
    }
}
