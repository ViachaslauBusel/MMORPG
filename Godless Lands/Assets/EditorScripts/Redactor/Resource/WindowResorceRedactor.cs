#if UNITY_EDITOR
using Items;
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Resource
{
    public class WindowResorceRedactor : Window
    {
        private ResourceEditor selectEditor;
        private ItemsRegistry items;

        [MenuItem("Window/Resources")]
        public static void ShowWindow()
        {
            WindowResorceRedactor window = EditorWindow.GetWindow<WindowResorceRedactor>(false, "Resources");
            window.minSize = new Vector2(600.0f, 320.0f);

        }

        private new void OnEnable()
        {
            base.OnEnable();
            string path = PlayerPrefs.GetString("RedactorResourcesResourcesList");
            objectList = AssetDatabase.LoadAssetAtPath<ResourceList>(path);

            path = PlayerPrefs.GetString("RedactorItemsList");
            items = AssetDatabase.LoadAssetAtPath<ItemsRegistry>(path);
        }
        protected override void DrawObject(System.Object obj)
        {
            Fabric resource = obj as Fabric;
            if (resource == null) return;

            GUILayout.Label("ID: " + resource.id);

            Texture2D texture = AssetPreview.GetAssetPreview(resource.prefab);
            TextureUtil.SetTransparent(texture);
            GUILayout.Label(texture, GUILayout.Width(90), GUILayout.Height(90));

          
            GUILayout.Label("Имя: " + resource.name);
            GUILayout.EndArea();
        }

        protected override void CreateObject()
        {     
            Fabric resource = new Fabric();
            SelectObject(resource);
            objectList.Add(resource);
        }
        protected override void SelectObject(object obj)
        {
            if (selectEditor == null) selectEditor = ScriptableObject.CreateInstance<ResourceEditor>();
            selectEditor.Select(obj, items);
            Selection.activeObject = selectEditor;
            selectEditor.Repaint();
        }
        protected override object GetSelectObject()
        {
            if (selectEditor == null) return null;
            return selectEditor.GetSelectObject();
        }
        protected override void Save()
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(objectList as ResourceList);
            AssetDatabase.SaveAssets();
           // ItemsExport.Export(itemsList.GetList());
            EditorUtility.DisplayDialog("Сохранение Ресурсов", "Ресурсы сохранены в контейнер: " + AssetDatabase.GetAssetPath(objectList), "OK");

            PlayerPrefs.SetString("RedactorResourcesResourcesList", AssetDatabase.GetAssetPath(objectList));
           // PlayerPrefs.SetString("RedactorItemsList", AssetDatabase.GetAssetPath(items));
        }
        protected override void EditableObject()
        {
            objectList = EditorGUILayout.ObjectField(objectList, typeof(ResourceList), false) as ObjectList;
        }

        protected override void RemoveSelectObject()
        {
            if (selectEditor != null && selectEditor.GetSelectObject() != null)
            {
                objectList.Remove(selectEditor.GetSelectObject());
            }
        }
    }
}
#endif