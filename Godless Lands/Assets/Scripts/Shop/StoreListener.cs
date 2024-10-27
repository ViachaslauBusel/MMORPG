using Inventory;
using Network.Core;
using Protocol;
using Protocol.Data.Items.Network;
using Protocol.MSG.Game.Trading.Store;
using RUCP;
using Shop.Models;
using Shop.UI;
using System;
using System.Linq;
using UnityEngine;

namespace Shop
{
    public class StoreListener : IDisposable
    {
        private NetworkManager _networkManager;
        private StoreModel _shopModel;
        private StoreWindow _shopWindow;
        private InventoryModel _inventoryModel;

        public StoreListener(NetworkManager networkManager, StoreModel shopModel, StoreWindow shopWindow, InventoryModel inventoryModel)
        {
            _networkManager = networkManager;
            _shopModel = shopModel;
            _shopWindow = shopWindow;
            _inventoryModel = inventoryModel;

            _networkManager.RegisterHandler(Opcode.MSG_STORE_TOGGLE_WINDOW, ShopToggleWindow);
        }

        private void ShopToggleWindow(Packet packet)
        {
            packet.Read(out MSG_STORE_TOGGLE_WINDOW_SC msg);

            if (msg.IsOpen)
            {
                if (_shopWindow.IsOpened) return;

                if (msg.ItemsForSale.Count > 0)
                {
                    foreach (var item in msg.ItemsForSale)
                    {
                        Debug.Log($"Item ID: {item.ID}, Price: {item.Price}");
                    }
                    _shopModel.ItemsForSale.Clear();
                    _shopModel.ItemsForSale.UpdateCapacity(0, 50);
                    _shopModel.ItemsForSale.UpdateItems(msg.ItemsForSale.Select((item, index) => new ItemSyncData
                    {
                        UniqueID = 0,
                        ItemID = item.ID,
                        Count = item.Price,
                        SlotIndex = index
                    }).ToList());

                    _shopModel.ItemsPurchasedByPlayer.Clear();
                    _shopModel.ItemsPurchasedByPlayer.UpdateCapacity(0, _inventoryModel.FreeSlots);
                    _shopModel.ItemsSoldByPlayer.Clear();
                    _shopModel.ItemsSoldByPlayer.UpdateCapacity(0, _inventoryModel.MaxSlots);
                }
                _shopWindow.Open();
            }
            else
            {
                _shopWindow.Hide();
            }
        }

        public void Dispose()
        {
            _networkManager.UnregisterHandler(Opcode.MSG_STORE_TOGGLE_WINDOW);
        }
    }
}
