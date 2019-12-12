#if UNITY_EDITOR
using NPCRedactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorld
{
    public class WindowVisibleNPC
    {
        private static NPCLoader npcLoader;
        private static int selectId = 0;

        public static void Draw()
        {
            if (npcLoader == null)
            {
                GameObject obj = GameObject.Find("MapEditor");
                if (obj != null)
                {
                    npcLoader = obj.GetComponent<NPCLoader>();
                }
            }
            if (npcLoader == null || npcLoader.npcs == null) return;
            foreach (NPCDraw npcDraw in npcLoader.npcs)
            {
                GUILayout.Space(10.0f);
                GUILayout.BeginHorizontal();
                NPC _npc = WindowSetting.NPCList.GetNPC(npcDraw.worldNPC.id);
                GUILayout.Label(_npc.name + ": " + _npc.id);

                GUI.enabled = selectId != npcDraw.worldNPC.GetHashCode();
                if (GUILayout.Button("Select", GUILayout.Width(60.0f)))
                {
                    Selection.activeObject = npcDraw.gameObject;
                    selectId = npcDraw.worldNPC.GetHashCode();
                    return;
                }
                GUI.enabled = true;

                if (GUILayout.Button("Delet", GUILayout.Width(60.0f)))
                {
                    WindowSetting.WorldNPCList.Remove(npcDraw.worldNPC);//Удалить монстра из списка монстров карты
                   NPCVisibleSceneGUI.UpdateNPCLoader();//Обновить монстров на сцене
                    return;
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif
