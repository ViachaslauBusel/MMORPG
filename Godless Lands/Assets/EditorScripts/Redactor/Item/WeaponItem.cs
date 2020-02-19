using Recipes;
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
        public int minDamege;
        public int maxDamage;
        public float speed;
        public List<Piece> pieces;//Компоненты на который распадается предмет после нейдачной заточки

        public WeaponItem()
        {
            pieces = new List<Piece>();
        }
    }
}