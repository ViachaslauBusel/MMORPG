using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Shop.UI
{
    public class TotalPurchaseItemPrice : TotalItemPriceCalculator
    {
        private StoreModel _storeModel;

        [Inject]
        private void Construct(StoreModel storeModel)
        {
            _storeModel = storeModel;
        }

        protected override void HandleWindowOpen()
        {
            SetItemStorage(_storeModel.ItemsPurchasedByPlayer);
        }

        protected override void HandleWindowClose()
        {
            SetItemStorage(null);
        }
    }
}
