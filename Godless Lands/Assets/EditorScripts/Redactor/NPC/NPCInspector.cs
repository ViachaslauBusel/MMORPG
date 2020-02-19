#if UNITY_EDITOR
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCRedactor
{
    [CustomEditor(typeof(NPCEditor))]
    [CanEditMultipleObjects]
    public class NPCInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            NPCPrefab npc = ((NPCEditor)target).npc;

            GUILayout.Space(15.0f);

            GUILayout.Label("ID: " + npc.id);

            GUILayout.Space(10.0f);

            npc.prefab = EditorGUILayout.ObjectField("Prefab", npc.prefab, typeof(GameObject), false) as GameObject;

            npc.name = EditorGUILayout.TextField("name:", npc.name);
        }
    }
}
#endif