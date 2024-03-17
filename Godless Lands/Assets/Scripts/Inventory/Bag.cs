using Items;
using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    private ItemsFactory _itemsFactory;
    private Item[] _items;
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

    public IReadOnlyCollection<Item> Items => _items;

    public Bag(ItemsFactory itemsFactory)
    {
        _itemsFactory = itemsFactory;
    }

    internal void UpdateCapacity(int currentItemsCount, int maxItemsCount)
    {
        _currentItemsCount = currentItemsCount;
        
        if (_items == null || _items.Length != maxItemsCount)
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
                    newItems[i] = null;
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

        foreach (var item in items)
        {
            if (item.SlotIndex >= 0 && item.SlotIndex < _items.Length)
            {
                if (ShouldUpdateItem(_items[item.SlotIndex], item))
                {
                    _items[item.SlotIndex] = _itemsFactory.CreateItem(item);
                }
            }
            else Debug.LogError($"Slot index {item.SlotIndex} is out of range");
        }
        OnItemsChanged?.Invoke();
    }
    private bool ShouldUpdateItem(Item existingItem, ItemSyncData newItem)
    {
        return existingItem == null
            || existingItem.UniqueID != newItem.UniqueID
            || existingItem.Count != newItem.Count;
    }
}
