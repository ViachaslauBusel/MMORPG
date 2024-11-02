using Trade;
using Zenject;

namespace Cells.Trade
{
    public class PlayerTradeCell : ItemCell
    {
        private TradeInputHandler _tradeInputHandler;

        [Inject]
        private void Construct(TradeInputHandler tradeInputHandler)
        {
            _tradeInputHandler = tradeInputHandler;
        }

        public override bool IsInteractingWithCurrentCell(Cell cell)
        {
            return false;
        }

        public override void Use()
        {
            if(IsEmpty()) return;

            _tradeInputHandler.AddTradeItem(GetItem(), GetIndex());
        }
    }
}
