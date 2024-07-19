#if UNITY_EDITOR
using MonsterRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Walkers.Monster;

namespace OpenWorldEditor
{

    public class WindowMonsters
    {

        private static int page = 0;
        private static int monstersOnPage = 20;
        private static int selectIndex = -1;
        private static GameObject _editableMonster;
        private static float radius = 1.0f;
        private static MonsterDrawGizmos monsterDrawGizmos;


        public static MonsterData MonsterPaint { get { if (_editableMonster != null) return null; return null; } }
        public static GameObject editableMonster { set {
                radius = 1.0f;
                monsterDrawGizmos = value.AddComponent<MonsterDrawGizmos>();//скрипт для отрисовки радиуса спавна 
                _editableMonster = value;
            } }

        private static void EditMonster()
        {
       
            Selection.activeObject = _editableMonster;

            GUILayout.Space(20.0f);
            radius = EditorGUILayout.Slider("Radius: ", radius, 1, 20);
            monsterDrawGizmos.radius = radius;


            if (GUILayout.Button("Закрепить"))
            {
                WindowSetting.WorldMonsterList.Add(new WorldMonster(0, _editableMonster.transform.position, radius));
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(WindowSetting.WorldMonsterList);
                AssetDatabase.SaveAssets();
                GameObject.DestroyImmediate(_editableMonster);
                _editableMonster = null;
                MonsterVisibleSceneGUI.UpdateMonsterLoader();
            }

           

        }
    

        public static void Draw()
        {
            GUILayout.Space(20.0f);
            if (_editableMonster != null)//Подтверждение добавление монстра на карту
            {
                EditMonster();
                return;
            }

            if ( WindowSetting.WorldMonsterList == null)
            {
                EditorGUILayout.HelpBox("Список монстров не выбран", MessageType.Error);
                return;
            }
            GUILayout.Space(20.0f);
            for (int i = page * monstersOnPage; i < 0; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(":" );
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
