#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WindowMachine
    {
        private static int page = 0;
        private static int MachineOnPage = 20;
        private static int selectIndex = -1;
        private static GameObject _editableObject;
        private static float radius = 1.0f;
        private static MonsterDrawGizmos monsterDrawGizmos;

      /*  public static GameObject editableObject
        {
            set
            {
                radius = 1.0f;
                monsterDrawGizmos = value.AddComponent<MonsterDrawGizmos>();//скрипт для отрисовки радиуса спавна 
                _editableResource = value;
            }
        }
        */
        private static void EditResources()
        {


            if (EditorUtility.DisplayDialog("Attach Object", "Добавить " + _editableObject.name + " на карту?", "Да", "Нет"))
            {
                GameObject objPrefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(_editableObject);
                if (objPrefab == null) { _editableObject = null; return; }//Если у обьекта нет префаба
                WindowSetting.MachineList.Add(new Machine(objPrefab, _editableObject));
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(WindowSetting.MachineList);
                AssetDatabase.SaveAssets();
                GameObject.DestroyImmediate(_editableObject);
                _editableObject = null;
                MachineVisibleSceneGUI.UpdateResourceLoader();
            }



        }


        public static void Draw()
        {
            GUILayout.Space(20.0f);
            if (_editableObject != null)//Подтверждение добавление монстра на карту
            {
                EditResources();
            }

            if (WindowSetting.MachineList == null)
            {
                EditorGUILayout.HelpBox("Список станков не выбран", MessageType.Error);
                return;
            }
            GUILayout.Space(20.0f);

            if (GUILayout.Button("Attach select Object"))
            {
                _editableObject = Selection.activeObject as GameObject;

                if(_editableObject.GetComponent<Action>() == null)
                {
                    _editableObject = null;
                    return;
                }
            }
        }
    }
}
#endif