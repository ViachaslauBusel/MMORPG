#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld {
    public class MachineVisibleSceneGUI
    {
        private static MachineLoader resourcesLoader;
        public static void SceneGUI(MapLoader mapLoader)
        {
            if (resourcesLoader == null)
            {
                resourcesLoader = mapLoader.gameObject.AddComponent<MachineLoader>();
                resourcesLoader.Start();
            }

            resourcesLoader.UpdateGUI();
        }

        public static void UpdateResourceLoader()
        {
            if (resourcesLoader != null) resourcesLoader.CalculeteVisibleMonster();
        }

        public static void Destroy()
        {
            if (resourcesLoader != null)
            {
                resourcesLoader.Destroy();
                GameObject.DestroyImmediate(resourcesLoader);
            }
        }
    }
}
#endif