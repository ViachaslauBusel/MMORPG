using Items;
using Protocol.Data.Items;
using System;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel
    {
        private readonly ItemStorage _primaryBag;
        private readonly ItemStorage _secondaryBag;

        public int FreeSlots => PrimaryBag.FreeSlotsCount + SecondaryBag.FreeSlotsCount;
        public int MaxSlots => PrimaryBag.MaxItemsCount + SecondaryBag.MaxItemsCount;
        public ItemStorage PrimaryBag => _primaryBag;
        public ItemStorage SecondaryBag => _secondaryBag;

        public event Action OnInventoryUpdated;

        public InventoryModel(ItemsFactory itemsFactory)
        {
            _primaryBag = new ItemStorage(itemsFactory, ItemStorageType.PrimaryBag);
            _secondaryBag = new ItemStorage(itemsFactory, ItemStorageType.SecondaryBag);
        }

        internal void SignalInventoryUpdate()
        {
            OnInventoryUpdated?.Invoke();
        }

        internal Item FindItem(long uniqueID)
        {
            if (PrimaryBag.TryGetItemByUID(uniqueID, out Item item))
            {
                return item;
            }

            if (SecondaryBag.TryGetItemByUID(uniqueID, out item))
            {
                return item;
            }

            return null;
        }

        public Item GetItemByUID(long itemUID)
        {
            if (PrimaryBag.TryGetItemByUID(itemUID, out Item item)) return item;
            if (SecondaryBag.TryGetItemByUID(itemUID, out item)) return item;
            return null;
        }

        internal int GetItemCountByItemId(int iD)
        {
            return PrimaryBag.GetItemCountByItemId(iD) + SecondaryBag.GetItemCountByItemId(iD);
        }

        public bool IsItemLocked(long uniqueID)
        {
            return _primaryBag.IsItemLocked(uniqueID) || _secondaryBag.IsItemLocked(uniqueID);
        }

        internal void UnlockItem(long uniqueID)
        {
            if (_primaryBag.IsItemLocked(uniqueID))
            {
                _primaryBag.UnlockItem(uniqueID);
            }
            else if (_secondaryBag.IsItemLocked(uniqueID))
            {
                _secondaryBag.UnlockItem(uniqueID);
            }
            else
            {
                Debug.LogWarning($"Item {uniqueID} is not locked");
            }
        }

        internal void LockItem(long uniqueID)
        {
            if (_primaryBag.TryGetItemByUID(uniqueID, out Item item))
            {
                _primaryBag.LockItem(uniqueID);
            }
            else if (_secondaryBag.TryGetItemByUID(uniqueID, out item))
            {
                _secondaryBag.LockItem(uniqueID);
            }
            else
            {
                Debug.LogWarning($"Item {uniqueID} not found");
            }
        }
    }
}