using Items;
using Machines;
using Recipes;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class WorkbenchCell : ItemCell
    {
        private Workbench workbench;

        private new void Awake()
        {
            workbench = GetComponentInParent<Workbench>();
            base.Awake();
        }
        public override void Put(Cell cell)
        {
            if (cell == null) return;
            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null || itemCell.IsEmpty() || itemCell.GetItem().type != ItemType.Recipes) return;
            //  if (components.ConstainsItem(itemCell.GetItem().id)) return;//Если этот предмет уже есть в списке
            //   PutItem(itemCell.GetItem(), itemCell.GetCount(), itemCell.GetKey());//Установить иконку
            PutItem(itemCell.GetItem());

        }

        public void PutItem(Item item)
        {
            this.item = item;
            if (IsEmpty())
            {
                HideIcon();
                return;
            }
          
            icon.sprite = Sprite.Create(item.texture, new Rect(0.0f, 0.0f, item.texture.width, item.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            ShowIcon();
            workbench.Refresh(item);
        }

        public override void Abort()
        {
            workbench.Clear();
        }
    }
}