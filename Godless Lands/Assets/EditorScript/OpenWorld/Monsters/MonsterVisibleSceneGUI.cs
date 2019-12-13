#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldEditor
{
    public class MonsterVisibleSceneGUI
    {
        private static MonstersLoader monstersLoader;
        public static void SceneGUI(MapLoader mapLoader)
        {
            if(monstersLoader == null)
            {
                monstersLoader = mapLoader.gameObject.AddComponent<MonstersLoader>();
                monstersLoader.Start();
            }

            monstersLoader.UpdateGUI();
        }

        public static void UpdateMonsterLoader()
        {
            if (monstersLoader != null) monstersLoader.CalculeteVisibleMonster();
        }

        public static void Destroy() {
            if (monstersLoader != null)
            {
                monstersLoader.Destroy();
                GameObject.DestroyImmediate(monstersLoader);
            }
        }
    }
}
#endif