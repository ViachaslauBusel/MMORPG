using Protocol.Data.Items;
using Trade;
using Zenject;

namespace Cells.Trade
{
    public class OfferCell : ItemCell
    {
        private TradeInputHandler _tradeInputHandler;
        private bool _isPlayerCell;

        [Inject]
        private void Construct(TradeInputHandler tradeInputHandler)
        {
            _tradeInputHandler = tradeInputHandler;
        }

        public void Init(bool isPlayerCell, int index)
        {
            _isPlayerCell = isPlayerCell;
            base.Init(ItemStorageType.None, index);
        }

        public override bool IsInteractingWithCurrentCell(Cell cell)
        {
            return cell is PlayerTradeCell;
        }

        public override void Use()
        {
          
        }

        public override void Put(Cell cell)
        {
            if (cell == null || _isPlayerCell == false) return;
           
            if(cell is ItemCell itemCell && (itemCell.StorageType == ItemStorageType.PrimaryBag 
                                          || itemCell.StorageType == ItemStorageType.SecondaryBag))
            {
                _tradeInputHandler.AddTradeItem(itemCell.GetItem(), GetIndex());
            }
        }
    }
}