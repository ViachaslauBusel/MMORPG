using Protocol;
using Protocol.MSG.Game.Inventory;
using RUCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryListener
    {
        private NetworkManager _networkManager;
        private InventoryModel _inventoryModel;

        [Inject]
        public InventoryListener(NetworkManager networkManager, InventoryModel inventoryModel)
        {
            _networkManager = networkManager;
            _inventoryModel = inventoryModel;

            _networkManager.RegisterHandler(Opcode.MSG_INVENTORY_SYNC, OnInventorySync);
        }

        private void OnInventorySync(Packet packet)
        {
            packet.Read(out MSG_INVENTORY_SYNC_SC data);

            Bag bag = data.Bag switch
            {
                InventoryBagType.Primary => _inventoryModel.PrimaryBag,
                InventoryBagType.Secondary => _inventoryModel.SecondaryBag,
                _ => null
            };

            if(bag == null)
            {
                Debug.LogError($"Bag {data.Bag} not found");
                return;
            }

            bag.UpdateCapacity(data.CurrentItemsCount, data.MaxItemsCount);
            bag.UpdateWeight(data.CurrentWeight, data.MaxWeight);
            bag.UpdateItems(data.Items);
        }
    }
}
