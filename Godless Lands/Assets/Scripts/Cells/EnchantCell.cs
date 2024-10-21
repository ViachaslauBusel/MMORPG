using Inventory;
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
        private InventoryModel _inventoryModel;
        private ItemEnchant enchant;
        private long objectID = 0;

        [Inject]
        private void Construct(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }

        private void Awake()
        {
           // base.Awake();
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


  
          //  if (itemCell.IsEmpty() || !Equipment.Is(itemCell.GetItem())) return;

            PutItem(itemCell.GetItem());
            objectID = _item.UniqueID;

        }

        public void Clear()
        {
            objectID = 0;
            PutItem(null);
        }
        internal void Refresh()//Вызывается из инвентаря при обновлении предметов
        {

            Item item =  _inventoryModel.GetItemByUID(objectID);

            PutItem(item);
        }
    }
}