using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Cells
{
   
    public class EnchantCell : ItemCell
    {
        private InventoryWindow _inventoryWindow;
        private ItemEnchant enchant;
        private long objectID = 0;

        [Inject]
        private void Construct(InventoryWindow inventoryWindow)
        {
            _inventoryWindow = inventoryWindow;
        }

        private new void Awake()
        {
            base.Awake();
            enchant = GetComponentInParent<ItemEnchant>();
        }
        public override void Put(Cell cell)
        {

            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null)
            {
                itemCell = (cell as BarCell)?.GetItemCell();
                if (itemCell == null) return;
            }


  
            if (itemCell.IsEmpty() || !Equipment.Is(itemCell.GetItem())) return;

            PutItem(itemCell.GetItem());
            objectID = item.objectID;

        }

        public void Clear()
        {
            objectID = 0;
            PutItem(null);
        }
        internal void Refresh()//Вызывается из инвентаря при обновлении предметов
        {

            Item item =  _inventoryWindow.GetItemByObjectID(objectID);

            PutItem(item);
        }
    }
}