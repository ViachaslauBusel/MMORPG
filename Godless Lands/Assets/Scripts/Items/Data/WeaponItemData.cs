using Protocol.Data.Items;
using Skills.Data;
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


        public float SpeedAttack => _speedAttack;
        public int MaxDamage => _maxDamage;
        public int MinDamege => _minDamage;
        public SkillBranch WeaponType => _weaponType;

        public override ItemInfo ToServerData()
        {
            return new WeaponItemInfo(ID, IsStackable, Weight, MinDamege, MaxDamage, (int)(SpeedAttack * 1000f));
        }
    }
}
