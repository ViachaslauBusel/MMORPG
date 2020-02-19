#if UNITY_EDITOR
using NPCRedactor;
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    /// <summary>
    /// Отрисовка инструмента кисть в окне OpenWorldEditor
    /// </summary>
    public class WindowNPCBrush
    {
        private static int page = 0;
        private static int npcOnPage = 20;
        private static int selectIndex = -1;
        private static GameObject _editableNPC;


        /// <summary>
        /// Возвращает префаб НПЦ для выбранной кисти, если кисть не выбрана или есть обьект для закрепление null
        /// </summary>
        public static NPCPrefab NPCPrefab { get { if (WindowSetting.NPCList == null) return null; return WindowSetting.NPCList[selectIndex]; } }
        /// <summary>
        /// Текущий обьект для добовление на карту
        /// </summary>
        public static GameObject editableNPC
        {
            set
            {
                _editableNPC = value;
            }
            get { return _editableNPC; }
        }

        /// <summary>
        /// Режим закрепление НПЦ на карту
        /// </summary>
        public static bool IsFixationMode { get { return _editableNPC != null; } }

        /// <summary>
        /// Закрепление НПЦ на карте
        /// </summary>
        private static void EditNPC()
        {

            Selection.activeObject = _editableNPC;

            GUILayout.Space(20.0f);
 



            if (GUILayout.Button("Закрепить"))
            {
                WindowSetting.WorldNPCList.Add(new WorldNPC(WindowSetting.NPCList[selectIndex].id, _editableNPC.transform.position, _editableNPC.transform.rotation.eulerAngles.y));
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(WindowSetting.WorldNPCList);
                AssetDatabase.SaveAssets();
                GameObject.DestroyImmediate(_editableNPC);
                _editableNPC = null;
                NPCVisibleSceneGUI.UpdateNPCLoader();
            }



        }


        /// <summary>
        /// Отрисовка списка кистей
        /// </summary>
        public static void Draw()
        {
            GUILayout.Space(20.0f);
            if (_editableNPC != null)//Подтверждение добавление монстра на карту
            {
                EditNPC();
                return;
            }

            if (WindowSetting.NPCList == null || WindowSetting.WorldNPCList == null)
            {
                EditorGUILayout.HelpBox("Список монстров не выбран", MessageType.Error);
                return;
            }
            GUILayout.Space(20.0f);
            for (int i = page * npcOnPage; i < WindowSetting.NPCList.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(WindowSetting.NPCList[i].name + ":" + WindowSetting.NPCList[i].id);
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