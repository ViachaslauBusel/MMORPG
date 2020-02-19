#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using OpenWorld;

namespace OpenWorldEditor
{
   
    [CustomEditor(typeof(Map))]
    public class MapEditor : Editor
    {
        private string nameMap = "";
        private int error = 0;
       
        public override void OnInspectorGUI()
        {
            ShowError();
            Map map = target as Map;
            if (string.IsNullOrEmpty(map.mapName))//Карта не создана
            {
                EditorGUILayout.LabelField("Карта не создана");
                nameMap = EditorGUILayout.TextField("Имя:", nameMap);
                map.mapSize = EditorGUILayout.IntField("Размер карты в километрах:", map.mapSize);
                map.blocksCount = EditorGUILayout.IntSlider("Блоков на километр:", map.blocksCount, 1, 100);
                map.startWorld = EditorGUILayout.Vector2Field("Начало мира:", map.startWorld);
                map.height = EditorGUILayout.FloatField("Максимальная высота мира:", map.height);
                map.setHeight = EditorGUILayout.FloatField("Заданная высота мира:", map.setHeight);

                if (GUILayout.Button("Создать"))
                {

                    if (nameMap.Length < 3) { error = 1; return; }
                    if (map.mapSize <= 0) { error = 2; return; }
                    if (map.height <= 1.0f) { error = 5; return; }
                    if (map.setHeight < 0.0f || map.setHeight > map.height) { error = 6; return; }
                    if (Directory.Exists("Assets/Resources/" + nameMap)) { error = 3; return; }
                    map.blockSize = map.sizeKMBlock / map.blocksCount;
                    if (MapGeneration.GenerationWorld(nameMap, map))
                    {
                      
                        map.mapName = nameMap;
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(map), map.mapName);
                        AssetDatabase.SaveAssets();
                    }
                    else { error = 4; return; }

                    error = 0;
                }
               
                
            }
            else
            {
                
                EditorGUILayout.LabelField("Имя: " + map.mapName);
                EditorGUILayout.LabelField("Размер карты в километрах: "+ map.mapSize.ToString());
                EditorGUILayout.LabelField("Размер TRTile: " + map.blockSize.ToString());
                EditorGUILayout.LabelField("Блоков на километр: "+ map.blocksCount.ToString());
                EditorGUILayout.LabelField("Начало мира: " + map.startWorld.ToString());
                EditorGUILayout.LabelField("Максимальная высота мира: " + map.height.ToString());

                if (GUILayout.Button("Удалить"))
                {
                    if (EditorUtility.DisplayDialog("Удаление карты", "Удалить карту: " + map.mapName  + "?", "Да", "Нет"))
                    {
                        Directory.Delete("Assets/Resources/" + map.mapName, true);
                     

                       AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(map));
                        AssetDatabase.Refresh();
                    }
                }

            }
        }

        private void ShowError()
        {
            switch (error)
            {
                case 0:
                    break;
                case 1:
                    EditorGUILayout.HelpBox("Неверное имя", MessageType.Error);
                    break;
                case 2:
                    EditorGUILayout.HelpBox("Неверный размер", MessageType.Error);
                    break;
                case 3:
                    EditorGUILayout.HelpBox("Имя уже используется", MessageType.Error);
                    break;
                case 4:
                    EditorGUILayout.HelpBox("Ошибка при создании карты", MessageType.Error);
                    break;
                case 5:
                    EditorGUILayout.HelpBox("Незадана высота", MessageType.Error);
                    break;
                case 6:
                    EditorGUILayout.HelpBox("Неправильная задаваемая высота", MessageType.Error);
                    break;
                default:
                    EditorGUILayout.HelpBox("Неизвестная ошибка", MessageType.Error);
                    break;
            }
        }
    }
}
#endif