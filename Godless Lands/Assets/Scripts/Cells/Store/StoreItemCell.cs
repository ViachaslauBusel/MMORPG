using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cells.Store
{
    public class StoreItemCell : ItemCell
    {
        protected override void UpdateCount()
        {
            if (_countTxt == null) return;
            _countTxt.text = _item?.Count.ToString();
        }
    }
}
