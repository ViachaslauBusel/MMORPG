﻿using Protocol.MSG.Game.CombatMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CombatMode
{
    public class CombatModeController
    {
        private NetworkManager _networkManager;
        private InputManager _inputManager;

        public CombatModeController(NetworkManager networkManager, InputManager inputManager)
        {
            _networkManager = networkManager;
            _inputManager = inputManager;

            _inputManager.Enable();
            _inputManager.FindAction("SwitchCombat").performed += ctx => SwitchCombat();
        }

        private void SwitchCombat()
        {
            MSG_SWITCH_COMBAT_MODE_CS msg = new MSG_SWITCH_COMBAT_MODE_CS();
            _networkManager.Client.Send(msg);
        }
    }
}
