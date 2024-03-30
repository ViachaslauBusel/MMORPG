using Items;
using Protocol.Data.Items;
using System;

namespace Inventory
{
    public class InventoryModel
    {
        public Bag PrimaryBag { get; } 
        public Bag SecondaryBag { get; }

        public event Action OnInventoryUpdate;

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
    }
}