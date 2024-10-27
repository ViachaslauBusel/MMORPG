using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cells.Store
{
    //Инвентарь игрока
    public class SaleItemCell : ItemCell
    {
        override public bool IsInteractingWithCurrentCell(Cell cell)
        {
            return false;
        }
    }
}
