using Items.Data;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Cells.Store
{
    //Ячейка для предмета который  в списке продажи
    public class SoldItemCell : ItemCell
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
            return cell is SaleItemCell saleCell && saleCell.IsEmpty() == false && saleCell.GetItem().Data is not MoneyItemData;
        }

        public override void Put(Cell cell)
        {
            if (cell is ItemCell itemCell)
            {
                var inventory_item = itemCell.GetItem();
                Debug.Log($"Put item from store: {inventory_item.Data.IsStackable}");
                if (inventory_item.Data.IsStackable)
                {
                    _selectQuantityWindow.Subscribe(
                                          "How many pieces to move?",
                                                             (count) =>
                                                             {
                                                                 _storeModel.ItemsSoldByPlayer.AddItem(inventory_item.Data, count);
                                                             },
                                                             () => { }
                                                                            );
                }
                else
                {
                    _storeModel.ItemsSoldByPlayer.AddItem(inventory_item.Data, 1);
                }
            }
        }
    }
}
