using Items;
using Protocol.Data.Trading.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trading.Shop.Data
{
    [Serializable]
    public class ShopItemData
    {
        [SerializeField]
        private ItemData _item;

        public ItemData Item => _item;

        public ShopItemSData ToServerData()
        {
            return new ShopItemSData(_item.ID);
        }
    }

    [CreateAssetMenu(fileName = "TradingShopData", menuName = "ScriptableObjects/Shop/TradingShopData")]
    public class TradingShopData : ScriptableObject
    {
        [SerializeField]
        private List<ShopItemData> _itemsForSale;

        public TradingShopSData ToServerData()
        {
            return new TradingShopSData(_itemsForSale.Select(x => x.ToServerData()).ToList());
        }
    }
}
