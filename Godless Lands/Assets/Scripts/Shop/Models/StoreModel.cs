using Items;
using Items.Models;
using Protocol.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class StoreModel
    {
        // Items owned by the shop
        private ItemStorage _itemsForSale;
        // List of items the player buys from the shop
        private LocalItemStorage _itemsPurchasedByPlayer;
        // List of items the player sells to the shop
        private LocalItemStorage _itemsSoldByPlayer;

        public ItemStorage ItemsForSale => _itemsForSale;
        public LocalItemStorage ItemsPurchasedByPlayer => _itemsPurchasedByPlayer;
        public LocalItemStorage ItemsSoldByPlayer => _itemsSoldByPlayer;

        public StoreModel(ItemsFactory itemsFactory)
        {
            _itemsForSale = new ItemStorage(itemsFactory, Protocol.Data.Items.ItemStorageType.StoreForSale);
            _itemsPurchasedByPlayer = new LocalItemStorage(itemsFactory, Protocol.Data.Items.ItemStorageType.StorePurchasedByPlayer);
            _itemsSoldByPlayer = new LocalItemStorage(itemsFactory, Protocol.Data.Items.ItemStorageType.StoreSoldByPlayer);
        }


    }
}

