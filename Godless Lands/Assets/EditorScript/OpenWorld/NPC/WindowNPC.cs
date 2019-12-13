#if UNITY_EDITOR
using NPCRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WindowNPC
    {
        private static int page = 0;
        private static int npcOnPage = 20;
        private static int selectIndex = -1;
        private static GameObject _editableNPC;
        private static float radius = 1.0f;
        private static NPCDraw npcDrawGizmos;


        public static NPC NPCPaint { get { if (WindowSetting.NPCList == null || _editableNPC != null) return null; return WindowSetting.NPCList[selectIndex]; } }
        public static GameObject editableNPC
        {
            set
            {
                radius = 1.0f;
                npcDrawGizmos = value.AddComponent<NPCDraw>();//скрипт для отрисовки радиуса спавна 
                _editableNPC = value;
            }
        }

        private static void EditNPC()
        {

            Selection.activeObject = _editableNPC;

            GUILayout.Space(20.0f);
 



            if (GUILayout.Button("Закрепить"))
            {
                WindowSetting.WorldNPCList.Add(new WorldNPC(WindowSetting.NPCList[selectIndex].id, _editableNPC.transform.position, radius));
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(WindowSetting.WorldMonsterList);
                AssetDatabase.SaveAssets();
                GameObject.DestroyImmediate(_editableNPC);
                _editableNPC = null;
                MonsterVisibleSceneGUI.UpdateMonsterLoader();
            }



        }


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