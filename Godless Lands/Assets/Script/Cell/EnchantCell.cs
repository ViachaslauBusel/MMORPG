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
            if (cell.GetType() != typeof(ItemCell) && cell.GetType() != typeof(ArmorCell)) return;

            ItemCell itemCell = cell as ItemCell;

            if (cell.IsEmpty() || !Equipment.Is(itemCell.GetItem())) return;

            PutItem(itemCell.GetItem(), itemCell.GetCount());
            key = itemCell.GetKey();
        }


        internal void Refresh()//Вызывается из инвентаря при обновлении предметов
        {
            Item item = Inventory.GetItem(key);
            PutItem(item, (item == null) ? 0 : item.count);
        }
    }
}