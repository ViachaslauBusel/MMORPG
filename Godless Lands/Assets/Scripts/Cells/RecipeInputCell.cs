using Items;
using Machines;
using Recipes;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class RecipeInputCell : ItemCell
    {
        private RecipeCraftingWindow workbench;

        private new void Awake()
        {
            workbench = GetComponentInParent<RecipeCraftingWindow>();
            Init();
           // base.Awake();
        }
        public override void Put(Cell cell)
        {
            if (cell == null) return;
            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null || itemCell.IsEmpty() || itemCell.GetItem().Data.type != ItemType.Recipes) return;
            //  if (components.ConstainsItem(itemCell.GetItem().id)) return;//Если этот предмет уже есть в списке
            //   PutItem(itemCell.GetItem(), itemCell.GetCount(), itemCell.GetKey());//Установить иконку
            PutItem(itemCell.GetItem());

        }

        public override void PutItem(Item item)
        {
            if (item != null && item.Data != null && item.Data.serializableObj is RecipesItem recipesItem)
            {
                _item = item;
                workbench.SelectRecipeItem(item, recipesItem);
            }
            else
            {
                _item = null;
            }
          
            
            if (IsEmpty())
            {
                HideIcon();
                return;
            }

            UpdateIcon();
            ShowIcon();
          
        }

        public override void Abort()
        {
            workbench.ResetCraftingWindow();
        }
    }
}