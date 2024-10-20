﻿using Network.Core;
using Protocol;
using Protocol.MSG.Game.Equipment.MSG;
using Protocol.MSG.Game.Inventory;
using RUCP;
using System;
using Zenject;

namespace Equipment
{
    public class EquipmentListener : IDisposable
    {
        private NetworkManager _networkManager;
        private EquipmentModel _equipmentModel;

        [Inject]
        public EquipmentListener(NetworkManager networkManager, EquipmentModel equipmentModel)
        {
            _networkManager = networkManager;
            _equipmentModel = equipmentModel;

            _networkManager.RegisterHandler(Opcode.MSG_EQUIPMENT_SYNC, OnEquipmentSync);
        }

        private void OnEquipmentSync(Packet packet)
        {
            packet.Read(out MSG_EQUIPMENT_SYNC_SC data);

            _equipmentModel.UpdateItems(data.Items);
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_EQUIPMENT_SYNC);
        }
    }
}
