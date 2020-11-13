#if UNITY_EDITOR
using Items;
using Recipes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ItemsRedactor
{
    public class InspectorItemWeapon
    {
        

        public static void Draw(System.Object obj)
        {
            WeaponItem weaponItem = obj as WeaponItem;
            if (weaponItem == null) return;

            weaponItem.weaponType = (SkillBranch)EditorGUILayout.EnumPopup("Тип оружия", weaponItem.weaponType);
            weaponItem.minDamege = EditorGUILayout.IntField("мин. урон:", weaponItem.minDamege);
            weaponItem.maxDamage = EditorGUILayout.IntField("мин. урон:", weaponItem.maxDamage);
            weaponItem.speed = EditorGUILayout.FloatField("Скор. Атаки:", weaponItem.speed);

            if (GUILayout.Button("Add component"))
            {
                weaponItem.pieces.Add(new Piece());
            }

            RecipeInspector.DrawComponent(weaponItem.pieces);
        }
    }
}
#endif