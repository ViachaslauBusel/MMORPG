#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor { 
public class WindowVisibleMachine
{
        private static MachineLoader machineLoader;
        private static GameObject selectObj;

        public static void Draw()
        {
            if (machineLoader == null)
            {
                GameObject obj = GameObject.Find("MapEditor");
                if (obj != null)
                {
                    machineLoader = obj.GetComponent<MachineLoader>();
                }
            }
            if (machineLoader == null || machineLoader.Resources == null) return;
            foreach (ObjectDrawGizmos objectDraw in machineLoader.Resources)
            {
                GUILayout.Space(10.0f);
                GUILayout.BeginHorizontal();
                GUILayout.Label(objectDraw.name+ ": "+objectDraw.id);

                GUI.enabled = !objectDraw.gameObject.Equals(selectObj);
                if (GUILayout.Button("Select", GUILayout.Width(60.0f)))
                {
                    Selection.activeObject = selectObj = objectDraw.gameObject;
                    return;
                }
                GUI.enabled = true;

                if (GUILayout.Button("Delet", GUILayout.Width(60.0f)))
                {
                    WindowSetting.MachineList.Remove(objectDraw.id);//Удалить монстра из списка монстров карты
                    AssetDatabase.Refresh();
                    EditorUtility.SetDirty(WindowSetting.MachineList);
                    AssetDatabase.SaveAssets();
                    MachineVisibleSceneGUI.UpdateResourceLoader();//Обновить монстров на сцене
                    return;
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif