#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace OpenWorld {
    public class WindowMonsterActiveTools
    {
        private static int tools = 0;

        public static bool MontersDraw { get { return tools == 1; } }
        public static bool MontersVisible { get { return tools == 2; } }

        public static void Draw()
        {
            GUILayout.Space(15.0f);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            //Добавление обьекта на карту
            GUI.enabled = tools != 1;
            if (GUILayout.Button(EditorGUIUtility.IconContent("Animation.AddEvent"), EditorStyles.miniButtonLeft, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {

                tools = 1;
            }

            //Просмотр списка мобов
            GUI.enabled = tools != 2;
            if (GUILayout.Button(EditorGUIUtility.IconContent("UnityLogo"), EditorStyles.miniButtonRight, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {

                tools = 2;
                MonsterSceneGUI.Destroy();
            }


            GUI.enabled = true;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            switch (tools)
            {
                case 1:
                    WindowMonsters.Draw();
                    break;
                case 2:
                    WindowVisibleMonster.Draw();
                    break;
            }
        }
    }
}
#endif