using Protocol.Data.Items;

namespace Items.Models
{
    public class LocalItemStorage : ItemStorage
    {
        public LocalItemStorage(ItemsFactory itemsFactory, ItemStorageType storageType) : base(itemsFactory, storageType)
        {
        }

        public void AddItem(ItemData item, int count, bool putInStack = true)
        {
            if (putInStack && item.IsStackable)
            {
                for (int i = 0; i < _items.Length; i++)
                {
                    if (_items[i].IsEmpty == false && _items[i].Data.ID == item.ID)
                    {
                        _items[i] = new Item(_items[i].UniqueID, _items[i].Count + count, i, item);
                        UpdateWeight(CurrentWeight + item.Weight * count, MaxWeight);
                        NotifyItemChanged();
                        return;
                    }
                }
            }
            for (int i = 0; i < _items.Length; i++)
            {

                if (_items[i].IsEmpty)
                {
                    _items[i] = new Item(0, count, i, item);
                    UpdateCapacity(CurrentItemsCount + 1, _items.Length);
                    UpdateWeight(CurrentWeight + item.Weight * count, MaxWeight);
                    return;
                }
            }
        }
    }
}
