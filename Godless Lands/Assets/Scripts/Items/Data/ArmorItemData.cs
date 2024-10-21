using Protocol.Data.Items.Types;
using Protocol.Data.Stats;
using Protocol.MSG.Game.Equipment;
using System.Collections.Generic;
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

        public override ItemSData ToServerData()
        {
            return new ArmorItemSData(ID, IsStackable, Weight, Price, _equipmentType, _defense, _modifiers);
        }
    }
}
