using Protocol.Data.Items;
using Protocol.Data.Stats;
using Protocol.MSG.Game.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Items.Data
{
    public class ArmorItemData : ItemData
    {
        [SerializeField]
        private EquipmentType _equipmentType;
        [SerializeField]
        private int _defense;
        [SerializeField]
        private List<StatModifierData> _modifiers;



        public int Defense => _defense;
        public List<StatModifierData> Modifiers => _modifiers;

        public override ItemInfo ToServerData()
        {
            return new ArmorItemInfo(ID, IsStackable, Weight, _equipmentType, _defense, _modifiers);
        }
    }
}
