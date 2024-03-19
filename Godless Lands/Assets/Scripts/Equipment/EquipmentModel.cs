using Items;
using Protocol.Data.Items.Network;
using Protocol.MSG.Game.Equipment;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Equipment
{
    public class EquipmentModel
    {
        private ItemsFactory _itemsFactory;
        private Dictionary<EquipmentType, Item> _equip = new Dictionary<EquipmentType, Item>();

        public IReadOnlyCollection<Item> Items => _equip.Values;

        public event Action OnItemsChanged;

        public EquipmentModel(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory;
            foreach (EquipmentType type in Enum.GetValues(typeof(EquipmentType)))
            {
                _equip.Add(type, _itemsFactory.CreateEmptyItem(slotIndex: (int)type));
            }
        }

        internal void UpdateItems(List<EquipmentItemSyncData> items)
        {
            foreach (var itemData in items)
            {
                Item item = _itemsFactory.CreateItem(itemData.ItemID, itemData.UniqueID, 1, (int)itemData.EquipmentType);
                _equip[itemData.EquipmentType] = item;
            }
            OnItemsChanged?.Invoke();
        }
    }
}