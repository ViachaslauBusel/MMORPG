using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
   
    public class EnchantCell : ItemCell
    {
        private ItemEnchant enchant;

        private new void Awake()
        {
            base.Awake();
            enchant = GetComponentInParent<ItemEnchant>();
        }
        public override void Put(Cell cell)
        {


            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null) return;

            if (cell.IsEmpty() || !Equipment.Is(itemCell.GetItem())) return;

            PutItem(itemCell.GetItem());

        }


        internal void Refresh()//Вызывается из инвентаря при обновлении предметов
        {
            Item item = Inventory.GetItemByObjectID(GetObjectID());
            PutItem(item);
        }
    }
}