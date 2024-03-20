using Items;
using Protocol.Data.Items;
using System;

namespace Inventory
{
    public class InventoryModel
    {
        public Bag PrimaryBag { get; } 
        public Bag SecondaryBag { get; }


        public InventoryModel(ItemsFactory itemsFactory)
        {
            PrimaryBag = new Bag(itemsFactory, ItemStorageType.PrimaryBag);
            SecondaryBag = new Bag(itemsFactory, ItemStorageType.SecondaryBag);
        }

        internal int GetAllCount(int id)
        {
            throw new NotImplementedException();
        }
    }
}