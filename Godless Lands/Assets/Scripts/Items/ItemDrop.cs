using Items;
using ObjectRegistryEditor;
using System;
using UnityEngine;

[Serializable]
public class ItemDrop
{
    [SerializeField]
    private DataLink<ItemData> _item;
    [SerializeField, Range(0, 100)]
    private float _chance;
    [SerializeField]
    private int _minAmount;
    [SerializeField]
    private int _maxAmount;

    public int ID => _item.ID;
    public float Chance => _chance;
    public int MinAmount => _minAmount;
    public int MaxAmount => _maxAmount;

    public ItemDrop(int id, int minAmount, int maxAmount, float chance)
    {
        _item = new DataLink<ItemData>(id);
        _minAmount = minAmount;
        _maxAmount = maxAmount;
        _chance = chance;
    }
}