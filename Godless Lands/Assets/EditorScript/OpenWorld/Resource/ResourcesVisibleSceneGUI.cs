#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenWorld
{
    public class ResourcesVisibleSceneGUI
    {
        private static ResourcesLoader resourcesLoader;
        public static void SceneGUI(MapLoader mapLoader)
        {
            if (resourcesLoader == null)
            {
                resourcesLoader = mapLoader.gameObject.AddComponent<ResourcesLoader>();
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