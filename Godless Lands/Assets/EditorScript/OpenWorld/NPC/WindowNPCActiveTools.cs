#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WindowNPCActiveTools
    {
        private static int tools = 0;

        /// <summary>
        /// Инструмент кисть
        /// </summary>
        public static bool NPCDraw { get { return tools == 1; } }
        /// <summary>
        /// Инструмент вывод НПЦ закрепленных на карте
        /// </summary>
        public static bool NPCVisible { get { return tools == 2; } }

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

            //Просмотр списка NPC
            GUI.enabled = tools != 2;
            if (GUILayout.Button(EditorGUIUtility.IconContent("UnityLogo"), EditorStyles.miniButtonRight, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                tools = 2;
                SceneNPCBrush.Destroy();
            }


            GUI.enabled = true;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            switch (tools)
            {
                case 1:
                    WindowNPCBrush.Draw();//Список доступных НПЦ в качестве кисти
                    break;
                case 2:
                    WindowVisibleNPC.Draw();//Список доступных НПЦ на сцене
                    break;
            }
        }
    }
}
#endif