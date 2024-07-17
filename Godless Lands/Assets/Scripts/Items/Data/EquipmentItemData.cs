using Protocol.MSG.Game.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items.Data
{
    public class EquipmentItemData : ItemData
    {
        [SerializeField]
        private EquipmentType _equipmentType;

        public EquipmentType EquipmentType => _equipmentType;
    }
}
