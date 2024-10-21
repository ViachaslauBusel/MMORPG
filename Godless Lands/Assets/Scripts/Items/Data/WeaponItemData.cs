using Protocol.Data.Items.Types;
using Protocol.Data.Stats;
using Skills.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Data
{
    public class WeaponItemData : ItemData
    {
        [Header("Weapon Item Data")]
        [SerializeField]
        private int _minDamage;
        [SerializeField]
        private int _maxDamage;
        [SerializeField]
        private float _speedAttack;
        [SerializeField]
        private SkillBranch _weaponType;
        [SerializeField]
        private List<StatModifierData> _modifiers;


        public float SpeedAttack => _speedAttack;
        public int MaxDamage => _maxDamage;
        public int MinDamege => _minDamage;
        public SkillBranch WeaponType => _weaponType;

        public override ItemSData ToServerData()
        {
            return new WeaponItemSData(ID, IsStackable, Weight, Price, MinDamege, MaxDamage, (int)(SpeedAttack * 1000f), _modifiers);
        }
    }
}
