#if UNITY_EDITOR
using Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace OpenWorldEditor
{
    public class WindowVisibleResources
    {
        private static ResourcesLoader resourcesLoader;
        private static int selectId = 0;

        public static void Draw()
        {
            if (resourcesLoader == null)
            {
                GameObject obj = GameObject.Find("MapEditor");
                if (obj != null)
                {
                    resourcesLoader = obj.GetComponent<ResourcesLoader>();
                }
            }
            if (resourcesLoader == null || resourcesLoader.Resources == null) return;
            foreach (ResourcesDrawGizmos resourceDraw in resourcesLoader.Resources)
            {
                //GUILayout.Space(10.0f);
                //GUILayout.BeginHorizontal();
                //Fabric _fabric = WindowSetting.ResourcesList.GetFabric(resourceDraw.worldFabric.id);
                //GUILayout.Label(_fabric.name + ": " + _fabric.id);

                //GUI.enabled = selectId != resourceDraw.worldFabric.GetHashCode();
                //if (GUILayout.Button("Select", GUILayout.Width(60.0f)))
                //{
                //    Selection.activeObject = resourceDraw.gameObject;
                //    selectId = resourceDraw.worldFabric.GetHashCode();
                //    return;
                //}
                //GUI.enabled = true;

                //if (GUILayout.Button("Delet", GUILayout.Width(60.0f)))
                //{
                //    WindowSetting.WorldResourcesList.Remove(resourceDraw.worldFabric);//Удалить монстра из списка монстров карты
                //    ResourcesVisibleSceneGUI.UpdateResourceLoader();//Обновить монстров на сцене
                //    return;
                //}
                //GUILayout.EndHorizontal();
            }
        }
    }
}
#endif