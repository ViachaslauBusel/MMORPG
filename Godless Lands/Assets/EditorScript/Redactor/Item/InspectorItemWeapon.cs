#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Items
{
    public class InspectorItemWeapon
    {

        public static void Draw(System.Object obj)
        {
            WeaponItem weaponItem = obj as WeaponItem;
            if (weaponItem == null) return;

            weaponItem.weaponType = (SkillBranch)EditorGUILayout.EnumPopup("Тип оружия", weaponItem.weaponType);
            weaponItem.physicalAttack = EditorGUILayout.IntField("Физ. Атака:", weaponItem.physicalAttack);
            weaponItem.prickingDamage = EditorGUILayout.IntField("колющий:", weaponItem.prickingDamage);
            weaponItem.crushingDamage = EditorGUILayout.IntField("дробящий:", weaponItem.crushingDamage);
            weaponItem.choppingDamage = EditorGUILayout.IntField("рубящий:", weaponItem.choppingDamage);
            weaponItem.speed = EditorGUILayout.FloatField("Скор. Атаки:", weaponItem.speed);
        }
    }
}
#endif