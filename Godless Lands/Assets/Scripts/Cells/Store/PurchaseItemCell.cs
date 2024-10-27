using Items;
using Shop.Models;
using UnityEngine;
using Zenject;

namespace Cells.Store
{
    //Ячейка для предмета который  в списке покупок
    public class PurchaseItemCell : ItemCell
    {
        private SelectQuantityWindow _selectQuantityWindow;
        private StoreModel _storeModel;

        [Inject]
        private void Construct(SelectQuantityWindow selectQuantityWindow, StoreModel storeModel)
        {
            _selectQuantityWindow = selectQuantityWindow;
            _storeModel = storeModel;
        }

        public override bool IsInteractingWithCurrentCell(Cell cell)
        {
            return cell.GetType() == typeof(StoreItemCell);
        }

        public override void Put(Cell cell)
        {
            if (cell is StoreItemCell storeItemCell)
            {
                var store_item = storeItemCell.GetItem();
                Debug.Log($"Put item from store: {store_item.Data.IsStackable}");
                if (store_item.Data.IsStackable)
                {
                    _selectQuantityWindow.Subscribe(
                                          "How many pieces to move?",
                                                             (count) =>
                                                             {
                                                                 _storeModel.ItemsPurchasedByPlayer.AddItem(store_item.Data, count);
                                                             },
                                                             () => { }
                                                                            );
                }
                else
                {
                    _storeModel.ItemsPurchasedByPlayer.AddItem(store_item.Data, 1);
                }
            }
        }
    }
}
