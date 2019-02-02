using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class WeaponItem
    {

        // public string prefab;
        public SkillBranch weaponType;
        public int physicalAttack;
        public int prickingDamage;
        public int crushingDamage;
        public int choppingDamage;
        public float speed;
    }
}