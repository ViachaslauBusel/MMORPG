using Network.Core;
using Protocol.Data.Items;
using Protocol.MSG.Game.Trading.Store;
using System;
using System.Collections.Generic;

namespace Shop
{
    public class StoreController
    {
        private readonly NetworkManager _networkManager;

        public StoreController(NetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        public void BuyItems(List<ItemBundleNData> itemsToBuy)
        {
            var msg = new MSG_STORE_BUY_ITEMS_SC
            {
                Items = itemsToBuy
            };
            _networkManager.Client.Send(msg);
        }

        public void SellItems(List<ItemBundleNData> itemsToSell)
        {
            var msg = new MSG_STORE_SELL_ITEMS_SC
            {
                Items = itemsToSell
            };
            _networkManager.Client.Send(msg);
        }

        internal void CloseStore()
        {
            MSG_STORE_END_INTERACTION_CS request = new MSG_STORE_END_INTERACTION_CS();
            _networkManager.Client.Send(request);
        }
    }
}
