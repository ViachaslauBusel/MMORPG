using ObjectRegistryEditor;
using Protocol.Data.Items;
using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class ItemBundleLink
    {
        [SerializeField]
        private DataLink<ItemData> _item;
        [SerializeField]
        private int _amount;

        public ItemBundleLink(int id, int count)
        {
            _item = new DataLink<ItemData>(id);
            _amount = count;
        }

        public int ID => _item.ID;
        public int Amount => _amount;

        internal ItemBundleSData ToServerData()
        {
            return new ItemBundleSData(_item.ID, _amount);
        }
    }

    [Serializable]
    public class ItemBundleData
    {
        [SerializeField]
        private ItemData _item;
        [SerializeField]
        private int _amount;


        public ItemData Item => _item;
        public int Amount => _amount;

        internal ItemBundleSData ToServerData()
        {
            return new ItemBundleSData(_item.ID, _amount);
        }
    }
}
