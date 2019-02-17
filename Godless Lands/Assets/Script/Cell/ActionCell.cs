using Cells;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCell : ItemCell
{
    private Machine machine;
    private Components components;
    private ItemCell itemCell;

    private new void Awake()
    {
        base.Awake();
        machine = GetComponentInParent<Machine>();
        components = GetComponentInParent<Components>();
    }
    public override void Use()
    {
      
    }
    public override void Put(Cell cell)
    {
        if(itemCell != null) itemCell.refresh -= Refresh;
        if (cell == null) return;
        itemCell = cell as ItemCell;
        if (itemCell == null || itemCell.IsEmpty()) return;
        if (components.ConstainsItem(itemCell.GetItem().id)) return;
        PutItem(itemCell.GetItem());//Установить иконку
        machine.Refresh();
        itemCell.refresh += Refresh;
    }

    public void Refresh(ItemCell cell)
    {
        if (IsEmpty())
        {
            cell.refresh -= Refresh;
            return;
        }
        if(GetItem().id != cell.GetItem().id)
        {
            cell.refresh -= Refresh;
            Abort();
            return;
        }
    }

    public override void Abort()
    {
        PutItem(null);
        machine.Refresh();
    }
}
