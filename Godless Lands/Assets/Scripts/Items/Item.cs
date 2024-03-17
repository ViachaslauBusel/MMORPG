using Protocol.Data.Items.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    public class Item
    {
        private readonly long _uniqueId;
        private readonly int _count;
        private readonly int _slotIndex;
        private readonly ItemData _data;

        public long UniqueID => _uniqueId;
        public int Count => _count;
        public int SlotIndex => _slotIndex;
        public ItemData Data => _data;
        public bool IsEmpty => _data == null;

        public int EnchantLevel { get; internal set; }
        public int Durability { get; internal set; }
        public int MaxDurability { get; internal set; }

        public Item()
        {
            _slotIndex = -1;
        }

        public Item(long uniqueId, int count, int slotIndex, ItemData data)
        {
            _uniqueId = uniqueId;
            _count = count;
            _slotIndex = slotIndex;
            _data = data;
        }
    }
}
