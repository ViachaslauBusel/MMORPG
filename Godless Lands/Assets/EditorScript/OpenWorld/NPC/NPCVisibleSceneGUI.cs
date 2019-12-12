#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{
    public class NPCVisibleSceneGUI
    {
        private static NPCLoader npcLoader;
        public static void SceneGUI(MapLoader mapLoader)
        {
            if (npcLoader == null)
            {
                npcLoader = mapLoader.gameObject.AddComponent<NPCLoader>();
                npcLoader.Start();
            }

            npcLoader.UpdateGUI();
        }

        public static void UpdateNPCLoader()
        {
            if (npcLoader != null) npcLoader.CalculeteVisibleNPC();
        }

        public static void Destroy()
        {
            if (npcLoader != null)
            {
                npcLoader.Destroy();
                GameObject.DestroyImmediate(npcLoader);
            }
        }
    }
}
#endif