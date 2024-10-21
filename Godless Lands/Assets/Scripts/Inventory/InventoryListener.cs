using Network.Core;
using Protocol;
using Protocol.Data.Items;
using Protocol.MSG.Game.Inventory;
using RUCP;
using System;
using UnityEngine;
using Zenject;

namespace Inventory
{
    public class InventoryListener : IDisposable
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

            ItemStorage bag = data.Bag switch
            {
                ItemStorageType.PrimaryBag => _inventoryModel.PrimaryBag,
                ItemStorageType.SecondaryBag => _inventoryModel.SecondaryBag,
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

            _inventoryModel.SignalInventoryUpdate();
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_INVENTORY_SYNC);
        }
    }
}
