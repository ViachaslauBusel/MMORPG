#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Items;

namespace ItemsRedactor
{
    public class InspectorItemRestorePoints
    {
        public static void Draw(System.Object obj)
        {
            RestorePointsItem restorePoint = obj as RestorePointsItem;
            if (restorePoint == null) return;

            restorePoint.hp = EditorGUILayout.IntField("HP:", restorePoint.hp);
            restorePoint.mp = EditorGUILayout.IntField("MP:", restorePoint.mp);
            restorePoint.mp = EditorGUILayout.IntField("Stamina:", restorePoint.stamina);
        }
    }
}
#endif