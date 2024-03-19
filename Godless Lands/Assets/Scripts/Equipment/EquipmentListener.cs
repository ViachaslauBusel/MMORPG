using Inventory;
using Protocol;
using Protocol.MSG.Game.Equipment.MSG;
using Protocol.MSG.Game.Inventory;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Equipment
{
    public class EquipmentListener
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
    }
}
