using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cells.Store
{
    //Ячейка для предмета который  в списке покупок
    public class StoreItemCell : ItemCell
    {
        public override bool IsInteractingWithCurrentCell(Cell cell)
        {
            return false;
        }

        protected override void UpdateCount()
        {
            if (_countTxt == null) return;
            _countTxt.text = _item?.Count.ToString();
        }
    }
}
