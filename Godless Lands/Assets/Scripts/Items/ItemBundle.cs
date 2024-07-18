using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items
{
    [Serializable]
    public class ItemBundle
    {
        [SerializeField]
        private DataLink<ItemData> _item;
        [SerializeField]
        private int _amount;

        public ItemBundle(int id, int count)
        {
            _item = new DataLink<ItemData>(id);
            _amount = count;
        }

        public int ID => _item.ID;
        public int Amount => _amount;
    }
}
