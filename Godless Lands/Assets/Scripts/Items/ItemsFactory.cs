using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items
{
    public class ItemsFactory : MonoBehaviour
    {
        [SerializeField]
        private ItemsRegistry _itemsContainer;

        internal Item CreateEmptyItem(long uid = 0, int count = 0, int slotIndex = -1)
        {
            return new Item(uid, count, slotIndex, null);
        }

        internal Item CreateItem(int id, long uid = 0, int count = 0, int slotIndex = -1)
        {
           ItemData item = _itemsContainer.GetObjectByID(id);
            return new Item(uid, count, slotIndex, item);
        }

        internal Item CreateItem(ItemSyncData item)
        {
            ItemData data = _itemsContainer.GetObjectByID(item.ItemID);
            return new Item(item.UniqueID, item.Count, item.SlotIndex, data);
        }

        public ItemData GetItemData(int id)
        {
            return _itemsContainer.GetObjectByID(id);
        }
    }
}
