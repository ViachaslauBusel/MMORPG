using Items;
using Protocol.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComponentsDrawer : MonoBehaviour
{
    private ItemStorageType _type;
    private SmelterCell[] _cells;

    public void Init(ItemStorageType type)
    {
        _type = type;
    }

    private void Start()
    {
        _cells = GetComponentsInChildren<SmelterCell>();
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].Init(_type, i);
        }
    }

    public int Length => _cells.Count(c => !c.IsEmpty());

    public bool ConstainsItem(int id) => _cells.Any(c => !c.IsEmpty() && c.ID() == id);

    public void Clear() => Array.ForEach(_cells, c => c.PutItem(null));

    public SmelterCell[] GetCells() => _cells;
    

    internal void UpdateContent(IReadOnlyCollection<Item> components)
    {
        if(components.Count != _cells.Length)
        {
            Debug.LogError("Components count is not equal to cells count");
            return;
        }
        for (int i = 0; i < _cells.Length; i++)
        {
            _cells[i].PutItem(components.ElementAt(i));
        }
    }
}
