using System;

namespace Inventory
{
    public class InventoryModel
    {
        public Bag PrimaryBag { get; } = new Bag();
        public Bag SecondaryBag { get; } = new Bag();

        internal int GetAllCount(int id)
        {
            throw new NotImplementedException();
        }
    }
}