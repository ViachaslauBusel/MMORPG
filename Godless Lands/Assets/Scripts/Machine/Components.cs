using Protocol.Data.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour
{
    public bool fuel;
    private ActionCell[] cells;

    private void Awake()
    {
        cells = GetComponentsInChildren<ActionCell>();
        for(int i=0; i<cells.Length; i++)
        {
            cells[i].Init(ItemStorageType.None, i);
            cells[i].fuel = fuel;
        }
    }

    public int Length
    {
        get { int len = 0;
            foreach(ActionCell cell in cells)
            {
                if (!cell.IsEmpty()) len++;
            }
            return len;
        }
    }

    public bool ConstainsItem(int id)
    {
        print("ConstainsItem i");
        foreach (ActionCell cell in cells)
        {
            if (!cell.IsEmpty() && cell.ID() == id) return true;
        }
        return false;
    }

    public void Clear()
    {
        foreach (ActionCell cell in cells)
        {
            cell.PutItem(null);
        }
    }

    public ActionCell[] GetCells()
    {
        return cells;
    }

    public ActionCell GetCell(int index)
    {
        if (index < 0 || index >= cells.Length) return null;
        return cells[index];
    }
}
