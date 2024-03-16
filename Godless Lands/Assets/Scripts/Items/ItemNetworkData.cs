using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public class ItemNetworkData
    {
        private readonly long _uniqueId;
        private readonly int _itemId;
        private readonly int _count;
        private readonly int _slotIndex;

        public long UniqueID => _uniqueId;
        public int ItemID => _itemId;
        public int Count => _count;
        public int SlotIndex => _slotIndex;
        public bool IsEmpty => _itemId == 0;

        public ItemNetworkData()
        {
            _slotIndex = -1;
        }

        public ItemNetworkData(ItemSyncData item)
        {
            _uniqueId = item.UniqueID;
            _itemId = item.ItemID;
            _count = item.Count;
            _slotIndex = item.SlotIndex;
        }
    }
}
