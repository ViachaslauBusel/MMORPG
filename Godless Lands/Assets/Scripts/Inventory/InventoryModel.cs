using Items;
using Protocol.Data.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryModel
    {
        private HashSet<long> _lockedItems = new HashSet<long>();

        public Bag PrimaryBag { get; } 
        public Bag SecondaryBag { get; }

        public event Action OnInventoryUpdate;
        public event Action OnLockUpdate;

        public InventoryModel(ItemsFactory itemsFactory)
        {
            PrimaryBag = new Bag(itemsFactory, ItemStorageType.PrimaryBag);
            SecondaryBag = new Bag(itemsFactory, ItemStorageType.SecondaryBag);
        }

        internal void SignalInventoryUpdate()
        {
            OnInventoryUpdate?.Invoke();
        }

        internal Item FindItem(long uniqueID)
        {
           if(PrimaryBag.TryFindItem(uniqueID, out Item item))
            {
                return item;
            }

            if(SecondaryBag.TryFindItem(uniqueID, out item))
            {
                return item;
            }

            return null;
        }

        internal int GetItemCountByItemId(int iD)
        {
            return PrimaryBag.GetItemCountByItemId(iD) + SecondaryBag.GetItemCountByItemId(iD);
        }
        

        public bool IsItemLocked(long uniqueID)
        {
            return _lockedItems.Contains(uniqueID);
        }

        internal void UnlockItem(long uniqueID)
        {
            if (_lockedItems.Remove(uniqueID) == false)
            {
                Debug.LogWarning($"Item {uniqueID} is not locked");
            }
        }

        internal void LockItem(long uniqueID)
        {
            if (_lockedItems.Add(uniqueID) == false)
            {
                Debug.LogWarning($"Item {uniqueID} is already locked");
            }
        }

        internal void SignalLockUpdate()
        {
            OnLockUpdate?.Invoke();
        }
    }
}