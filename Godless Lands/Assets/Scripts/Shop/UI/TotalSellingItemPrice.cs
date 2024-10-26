using Shop.Models;
using Shop.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Shop.UI
{
    public class TotalSellingItemPrice : TotalItemPriceCalculator
    {
        private StoreModel _storeModel;

        [Inject]
        private void Construct(StoreModel storeModel)
        {
            _storeModel = storeModel;
        }

        protected override void HandleWindowOpen()
        {
            SetItemStorage(_storeModel.ItemsSoldByPlayer);
        }

        protected override void HandleWindowClose()
        {
            SetItemStorage(null);
        }
    }
}
