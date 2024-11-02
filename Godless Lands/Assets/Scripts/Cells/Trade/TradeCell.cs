using Cells.Trade;
using RUCP;
using Zenject;

namespace Cells
{
    public class TradeCell : ItemCell
    {
        private SelectQuantityWindow _selectQuantityWindow;

        [Inject]
        private void Construct(SelectQuantityWindow selectQuantityWindow)
        {
            _selectQuantityWindow = selectQuantityWindow;
        }

        public override bool IsInteractingWithCurrentCell(Cell cell)
        {
            return cell is PlayerTradeCell;
        }

        /// <summary>
        /// Предложить предмет на обмен
        /// </summary>
        public override void Use()
         {
            if (IsEmpty()) return;
            //Открыть окно для ввода количества предметов
            _selectQuantityWindow.Subscribe(
                "Сколько штук переместить?",
                (count) =>
                {
                //TODO msg
                    //Packet nw = new Packet(Channel.Reliable);
                    //nw.WriteType(Types.ItemTrade);
                    //nw.WriteInt(item.objectID);
                    //nw.WriteInt(count);
                    //NetworkManager.Send(nw);
                },
                () => { }
                );
        }

        
        public void PutItemCell(ItemCell itemCell)
        {
            _index = itemCell.GetIndex();
            PutItem(itemCell.GetItem());
        }

        public override void Put(Cell cell)
        {
        }
    }
}