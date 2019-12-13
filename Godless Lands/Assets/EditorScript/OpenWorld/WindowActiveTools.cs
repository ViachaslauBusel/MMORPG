#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WindowActiveTools
    {

        
        public static int active = 0;

        public static bool ActiveTerrain() { return active == 1; }

        public static void Load()
        {
            active = PlayerPrefs.GetInt("ActiveTools", 0);
        }

        public static int Active()
        {

           
            GUILayout.Space(25.0f);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            //Отрисовка и редактирование террейна
            GUI.enabled = active != 1;
            if(GUILayout.Button(EditorGUIUtility.IconContent("Terrain Icon"), EditorStyles.miniButtonLeft, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 1;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            //Отрисовка и редактирование монстров нпц
            GUI.enabled = active != 2;
            if(GUILayout.Button(EditorGUIUtility.IconContent("Avatar Icon"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 2;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            //Отрисовка и редактирование ресурсов
            GUI.enabled = active != 6;
            if (GUILayout.Button(EditorGUIUtility.IconContent("Avatar Icon"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 6;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            //Отрисовка и редактирование станков
            GUI.enabled = active != 7;
            if (GUILayout.Button(EditorGUIUtility.IconContent("Avatar Icon"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 7;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            //Отрисовка и редактирование обьектов на карте
            GUI.enabled = active != 3;
            if(GUILayout.Button(EditorGUIUtility.IconContent("Prefab Icon"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 3;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            //Отрисовка и редактирование обьектов на карте
            GUI.enabled = active != 4;
            if (GUILayout.Button(EditorGUIUtility.IconContent("BuildSettings.Web"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 4;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            //Настройка редактора, путей до ассетов
            GUI.enabled = active != 5;
            if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 5;
                PlayerPrefs.SetInt("ActiveTools", active);
            }

            GUI.enabled = true;
            //Отключить все
            if (GUILayout.Button(EditorGUIUtility.IconContent("PauseButton"), EditorStyles.miniButtonRight, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                active = 0;
                PlayerPrefs.SetInt("ActiveTools", active);
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            return active;
        }
    }
}
#endif