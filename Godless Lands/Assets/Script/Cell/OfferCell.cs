using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cells
{
    public class OfferCell : ItemCell
    {
        private bool open;

        public override void Use()
        {
          
        }

        public override void Put(Cell cell)
        {
            if (cell == null) return;
            if (cell.GetType() != typeof(TradeCell)) return;
            TradeCell tradeCell = cell as TradeCell;
            cell.Use();
        }

        public void SetOpen(bool open)
        {
            this.open = open;
        }
    }
}