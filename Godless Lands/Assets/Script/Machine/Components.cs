using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour
{
    private ActionCell[] cells;

    private void Awake()
    {
        cells = GetComponentsInChildren<ActionCell>();
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
        foreach (ActionCell cell in cells)
        {
            if (!cell.IsEmpty() && cell.GetItem().id == id) return true;
        }
        return false;
    }
}
