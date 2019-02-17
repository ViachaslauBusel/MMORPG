#if UNITY_EDITOR
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Resource
{
    [CustomEditor(typeof(ResourceEditor))]
    [CanEditMultipleObjects]
    public class ResourceInspector : Editor
    {
        private static ItemsList itemsList;
        public override void OnInspectorGUI()
        {
            Fabric resource = ((ResourceEditor)target).GetSelectObject() as Fabric;
            itemsList = ((ResourceEditor)target).itemsList;

            GUILayout.Space(15.0f);

            GUILayout.Label("ID: " + resource.id);

            GUILayout.Space(10.0f);

            resource.prefab = EditorGUILayout.ObjectField("Prefab", resource.prefab, typeof(GameObject), false) as GameObject;

            resource.name = EditorGUILayout.TextField("name:", resource.name);

            resource.startSpawn = EditorGUILayout.FloatField("start Spawn:", resource.startSpawn);
            resource.timeSpawn = EditorGUILayout.FloatField("time Spawn:", resource.timeSpawn);

            if (GUILayout.Button("Add drop"))
            {
                resource.drops.Add(new Drop());
            }
            
            DrawComponent(resource.drops);
        }

        private void DrawComponent(List<Drop> drops)
        {
            Drop remove = null;
            for (int i = 0; i < drops.Count; i++)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("ID:", GUILayout.Width(20.0f));
                drops[i].itemID = EditorGUILayout.IntField(drops[i].itemID);

                EditorGUILayout.LabelField("chance:", GUILayout.Width(60.0f));
                drops[i].chance = EditorGUILayout.FloatField(drops[i].chance);
                GUILayout.EndHorizontal();
                if (DrawItemComponent(drops[i].itemID)) { remove = drops[i]; }
                GUILayout.EndVertical();
            }
            if (remove != null)
            {
                drops.Remove(remove);
            }
        }

        private bool DrawItemComponent(int id)
        {
            if (itemsList == null) return false;
            Item item = itemsList.GetItem(id);
            GUILayout.BeginHorizontal();
            if (item != null)
            {
                GUILayout.Label(item.texture, GUILayout.Width(20), GUILayout.Height(20));
                GUILayout.Label(item.nameItem);
            }
            else
            {
                GUILayout.FlexibleSpace();
            }
            if (GUILayout.Button("remove", GUILayout.Width(60))) return true;
            GUILayout.EndHorizontal();
            return false;
        }
    }
}
#endif