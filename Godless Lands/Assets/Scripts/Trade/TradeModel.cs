using Items;
using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Trade
{
    internal class TradeModel
    {
        private ItemsFactory _itemsFactory;
        private Item[] _playerTradeItems = new Item[0];
        private Item[] _partnerTradeItems = new Item[0];
        private int _currentTradeID;

        public Item[] PlayerTradeItems => _playerTradeItems;
        public Item[] PartnerTradeItems => _partnerTradeItems;

        public event Action OnItemsChanged;

        public TradeModel(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
        }

        internal void Update(int tradeID, bool isOwner, int bagSize, List<ItemSyncData> items)
        {
            if(_currentTradeID != tradeID)
            {
                _currentTradeID = tradeID;
                _playerTradeItems = new Item[0];
                _partnerTradeItems = new Item[0];
            }

            UpdateBag(ref GetTradeItemsForSync(isOwner), bagSize, items);

            OnItemsChanged?.Invoke();
        }

        private void UpdateBag(ref Item[] bagForSync, int bagSize, List<ItemSyncData> items)
        {
            if(bagForSync.Length != bagSize)
            {
                Array.Resize(ref bagForSync, bagSize);
            }

            foreach (var item in items)
            {
                if (item.SlotIndex < 0 || item.SlotIndex >= bagSize)
                {
                    Debug.LogError($"[TradeModel] Item with invalid slot index {item.SlotIndex}");
                    continue;
                }

                bagForSync[item.SlotIndex] = _itemsFactory.CreateItem(item);
            }
        }

        private ref Item[] GetTradeItemsForSync(bool isOwner)
        {
            if (isOwner)
            {
                return ref _playerTradeItems;
            }

            return ref _partnerTradeItems;
        }
    }
}
