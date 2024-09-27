﻿using Network.Core;
using Protocol;
using Protocol.Data.Units.CraftingStation;
using Protocol.MSG.Game.Workbench;
using RUCP;
using System;

namespace Workbench
{
    public class WorkbenchListener : IDisposable
    {
        private NetworkManager _networkManager;

        public event Action<CraftingStationType, bool, bool> OnWindowCommand;

        public WorkbenchListener(NetworkManager networkManager)
        {
            _networkManager = networkManager;

            _networkManager.RegisterHandler(Opcode.MSG_WORKBENCH_TOGGLE_WINDOW, ToggleWindow);
        }

        private void ToggleWindow(Packet packet)
        {
            packet.Read(out MSG_WORKBENCH_TOGGLE_WINDOW_SC msg);

            OnWindowCommand?.Invoke(msg.WorkbenchType, msg.IsOpen, msg.IsReadyForWork);
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_WORKBENCH_TOGGLE_WINDOW);
        }
    }
}
