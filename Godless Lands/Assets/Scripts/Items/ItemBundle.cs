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

        public int Item => _item.ID;
        public int Amount => _amount;
    }
}
