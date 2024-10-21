using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ShopModel
    {
        // Items owned by the shop
        private ItemStorage _itemsForSale;
        // List of items the player buys from the shop
        private ItemStorage _itemsPurchasedByPlayer;
        // List of items owned by the player
        private ItemStorage _playerItems;
        // List of items the player sells to the shop
        private ItemStorage _itemsSoldByPlayer;
    }
}

