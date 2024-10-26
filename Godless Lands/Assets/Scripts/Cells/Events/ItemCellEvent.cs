using Items;
using Items.Data;
using UnityEngine;

namespace Cells
{
    public class ItemCellEvent : CellEvent
    {
        public override void RightClick()//Использвоние предмета
        {
            if (cell.GetType() == typeof(ItemCell) && !cell.IsEmpty())
            {
                ItemCell itemCell = cell as ItemCell;
                ItemInteractionFactory.CreateContextMenuForItem(itemCell, cellParent);

                //   Button use =
            }
            else cell.Use();
        }

        public override void ShowInfo()
        {
            if (cell.IsEmpty()) return;
            ItemCell itemCell = cell as ItemCell;
            if (itemCell == null) return;

            informer = ItemInteractionFactory.CreateItemInfoPanel(cellParent.parent, itemCell.GetItem());
        }
    }
}
