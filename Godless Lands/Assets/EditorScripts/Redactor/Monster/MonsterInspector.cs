#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MonsterRedactor
{
    [CustomEditor(typeof(MonsterEditor))]
    [CanEditMultipleObjects]
    public class MonsterInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            Monster monster = ((MonsterEditor)target).monster;

            GUILayout.Space(15.0f);

            GUILayout.Label("ID: " + monster.id);

            GUILayout.Space(10.0f);

           monster.prefab =  EditorGUILayout.ObjectField("Prefab", monster.prefab, typeof(GameObject), false) as GameObject;

           monster.name = EditorGUILayout.TextField("name:", monster.name);

           monster.hp = EditorGUILayout.IntField("HP: ", monster.hp);


        }
    }
}
#endif