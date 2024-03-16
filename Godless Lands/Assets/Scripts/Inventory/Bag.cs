using Items;
using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    private ItemNetworkData[] _items;
    private int _currentWeight;
    private int _maxWeight;

    public event Action OnCapacityChanged;
    public event Action OnWeightChanged;
    public event Action OnItemsChanged;

    public int MaxCells => _items.Length;
    public int CurrentWeight => _currentWeight;
    public int MaxWeight => _maxWeight;

    public IReadOnlyCollection<ItemNetworkData> Items => _items;

    internal void UpdateCapacity(int maxCells)
    {
        Debug.Log($"UpdateCapacity {maxCells}");
        
        if (_items == null || _items.Length != maxCells)
        {
            var newItems = new ItemNetworkData[maxCells];
            for (int i = 0; i < maxCells; i++)
            {
                if (_items != null && i < _items.Length)
                {
                    newItems[i] = _items[i];
                }
                else
                {
                    newItems[i] = new ItemNetworkData();
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
                Debug.Log($"UpdateItems {item.SlotIndex}:{item.ItemID}");
                _items[item.SlotIndex] = new ItemNetworkData(item);
            } 
            else Debug.LogError($"Slot index {item.SlotIndex} is out of range");
        }
        OnItemsChanged?.Invoke();
    }
}
