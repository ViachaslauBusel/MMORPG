using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class BarCellEvent : CellEvent
    {
        private bool locker = true;

        private new void Start()
        {
            base.Start();
            doubleClick = false;
        }

        public override void ShowInfo()
        {
            if (cell.IsEmpty() || locker) return;

            BarCell barCell = cell as BarCell;
            if (barCell == null) return;
            if (barCell.GetSkill() != null)
            {
                informer = SkillCellEvent.SkillInfo(barCell.GetSkill(), cellParent.parent);
            }
            else if(barCell.GetItem() != null)
            {
                informer = ItemCellEvent.ItemInfo(cellParent.parent, barCell.GetItem());
            }
            //    (cell as BarCell).GetTargetCell().GetComponent<CellEvent>().ShowInfo();
        }

      /*  public override void HideInfo()
        {
            if (cell.IsEmpty()) return;
          //  (cell as BarCell).GetTargetCell().GetComponent<CellEvent>().HideInfo();
        }*/

        public void SetLock(bool locker)
        {
            this.locker = locker;
        }
    }
}