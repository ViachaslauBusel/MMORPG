#if UNITY_EDITOR
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace OpenWorldEditor
{
    public class WindowResources
    {
        private static int page = 0;
        private static int monstersOnPage = 20;
        private static int selectIndex = -1;
        private static GameObject _editableResource;
        private static float radius = 1.0f;
        private static MonsterDrawGizmos monsterDrawGizmos;

        //Если включено добовление ресурса на карту
        public static Fabric ResourcePaint { get { if (WindowSetting.ResourcesList == null || _editableResource != null) return null; return WindowSetting.ResourcesList[selectIndex] as Fabric; } }
        public static GameObject editableResources
        {
            set
            {
                radius = 1.0f;
                monsterDrawGizmos = value.AddComponent<MonsterDrawGizmos>();//скрипт для отрисовки радиуса спавна 
                _editableResource = value;
            }
        }

        private static void EditResources()
        {

            Selection.activeObject = _editableResource;

            GUILayout.Space(20.0f);
            radius = EditorGUILayout.Slider("Radius: ", radius, 1, 20);
            monsterDrawGizmos.radius = radius;


            if (GUILayout.Button("Закрепить"))
            {
                WindowSetting.WorldResourcesList.Add(new WorldFabric((WindowSetting.ResourcesList[selectIndex]as Fabric).id, _editableResource.transform.position, radius));
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(WindowSetting.WorldResourcesList);
                AssetDatabase.SaveAssets();
                GameObject.DestroyImmediate(_editableResource);
                _editableResource = null;
                MonsterVisibleSceneGUI.UpdateMonsterLoader();
            }



        }


        public static void Draw()
        {
            GUILayout.Space(20.0f);
            if (_editableResource != null)//Подтверждение добавление монстра на карту
            {
                EditResources();
                return;
            }

            if (WindowSetting.ResourcesList == null || WindowSetting.WorldResourcesList == null)
            {
                EditorGUILayout.HelpBox("Список ресурсов не выбран", MessageType.Error);
                return;
            }
            GUILayout.Space(20.0f);
            for (int i = page * monstersOnPage; i < WindowSetting.ResourcesList.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(((Fabric)WindowSetting.ResourcesList[i]).name + ":" + ((Fabric)WindowSetting.ResourcesList[i]).id);
                GUI.enabled = i != selectIndex;

                if (GUILayout.Button("Select", GUILayout.Height(20.0f), GUILayout.Width(50.0f)))
                {
                    selectIndex = i;
                }

                GUI.enabled = true;
                GUILayout.EndHorizontal();
                GUILayout.Space(5.0f);
            }
        }
    }
}
    #endif