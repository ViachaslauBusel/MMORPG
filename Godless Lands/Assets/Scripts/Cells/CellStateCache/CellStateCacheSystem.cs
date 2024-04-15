using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cells.CellStateCache
{
    public class CellStateCacheSystem
    {
        /// <summary>
        /// Hide the item until the inventory will be updated
        /// If the response is not received, the item will show again
        /// </summary>
        /// <param name="itemCell"></param>
        /// <param name="v"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void HideItemUntilResponse(ItemCell itemCell, int v)
        {
            throw new NotImplementedException();
        }

        internal void ShowIfUIDUquals(Cell cell, long v, int v1)
        {
            throw new NotImplementedException();
        }

        internal void ShowItemUntilResponse(ItemCell itemCell, Item item, int v)
        {
            throw new NotImplementedException();
        }
    }
}
