using Items;
using Protocol.Data.Items;
using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bag
{
    private ItemStorageType _storageType;
    private ItemsFactory _itemsFactory;
    private Item[] _items = new Item[0];
    private int _currentWeight;
    private int _maxWeight;
    private int _currentItemsCount;

    public event Action OnCapacityChanged;
    public event Action OnWeightChanged;
    public event Action OnItemsChanged;

    public int CurrentItemsCount => _currentItemsCount;
    public int MaxItemsCount => _items.Length;
    public int CurrentWeight => _currentWeight;
    public int MaxWeight => _maxWeight;
    public ItemStorageType StorageType => _storageType;
    public IReadOnlyCollection<Item> Items => _items;

    public Bag(ItemsFactory itemsFactory, ItemStorageType storageType)
    {
        _storageType = storageType;
        _itemsFactory = itemsFactory;
    }

    internal void UpdateCapacity(int currentItemsCount, int maxItemsCount)
    {
        _currentItemsCount = currentItemsCount;
        
        if (_items.Length != maxItemsCount)
        {
            var newItems = new Item[maxItemsCount];
            for (int i = 0; i < maxItemsCount; i++)
            {
                if (_items != null && i < _items.Length)
                {
                    newItems[i] = _items[i];
                }
                else
                {
                    newItems[i] = _itemsFactory.CreateEmptyItem(slotIndex: i);
                }
            }
            _items = newItems;
        }
        OnCapacityChanged?.Invoke();
    }

    internal void UpdateWeight(int currentWeight, int maxWeight)
    {
        _currentWeight = currentWeight;
        _maxWeight = maxWeight;
        OnWeightChanged?.Invoke();
    }

    internal void UpdateItems(List<ItemSyncData> items)
    {
        if (_items == null)
        {
            Debug.LogError("Items is null");
            return;
        }

        foreach (var itemInfo in items)
        {
            if (itemInfo.SlotIndex >= 0 && itemInfo.SlotIndex < _items.Length)
            {
                if (ShouldUpdateItem(_items[itemInfo.SlotIndex], itemInfo))
                {
                    _items[itemInfo.SlotIndex] = _itemsFactory.CreateItem(itemInfo);
                }
            }
            else Debug.LogError($"Slot index {itemInfo.SlotIndex} is out of range");
        }
        OnItemsChanged?.Invoke();
    }
    private bool ShouldUpdateItem(Item existingItem, ItemSyncData newItem)
    {
        return existingItem == null
            || existingItem.UniqueID != newItem.UniqueID
            || existingItem.Count != newItem.Count;
    }

    internal bool TryFindItem(long uniqueID, out Item item)
    {
        item = Array.Find(_items, i => i != null && i.UniqueID == uniqueID);
        return item != null;
    }

    internal int GetItemCountByItemId(int itemId)
    {
        return _items.Where(i => i != null && i.Data != null && i.Data.ID == itemId).Sum(i => i.Count);
    }
}
