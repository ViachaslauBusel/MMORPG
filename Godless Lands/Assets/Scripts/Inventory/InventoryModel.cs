using Items;
using System;

namespace Inventory
{
    public class InventoryModel
    {
        public Bag PrimaryBag { get; } 
        public Bag SecondaryBag { get; }


        public InventoryModel(ItemsFactory itemsFactory)
        {
            PrimaryBag = new Bag(itemsFactory);
            SecondaryBag = new Bag(itemsFactory);
        }

        internal int GetAllCount(int id)
        {
            throw new NotImplementedException();
        }
    }
}