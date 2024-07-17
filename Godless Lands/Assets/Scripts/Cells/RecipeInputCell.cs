using Items;
using Items.Data;
using Workbench.UI.RecipeCrafting;

namespace Cells
{
    public class RecipeInputCell : ItemCell
    {
        private RecipeCraftingWindow _recipeCraftingWindow;

        private void Awake()
        {
            _recipeCraftingWindow = GetComponentInParent<RecipeCraftingWindow>();
            Init();
        }

        public override void Put(Cell cell)
        {
            if (cell == null) return;
            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null || itemCell.IsEmpty() || itemCell.GetItem().Data is not RecipeItemData) return;
            //  if (components.ConstainsItem(itemCell.GetItem().id)) return;//Если этот предмет уже есть в списке
            //   PutItem(itemCell.GetItem(), itemCell.GetCount(), itemCell.GetKey());//Установить иконку
            PutItem(itemCell.GetItem());

        }

        public override void PutItem(Item item)
        {
            if (item != null && item.Data != null && item.Data is RecipeItemData recipeData)
            {
                _item = item;
                _recipeCraftingWindow.SelectRecipeItem(recipeData);
            }
            else
            {
                _item = null;
            }
          
            
            if (IsEmpty())
            {
                Hide();
                return;
            }

            UpdateIcon();
            Show();
        }

        public override void Abort()
        {
            _recipeCraftingWindow.ResetCraftingWindow();
        }
    }
}