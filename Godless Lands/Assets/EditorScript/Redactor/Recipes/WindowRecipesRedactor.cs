#if UNITY_EDITOR
using Items;
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Recipes
{
    public class WindowRecipesRedactor : Window
    {
        private RecipeEditor selectEditor;
        private ItemsList items;

        [MenuItem("Window/Recipes")]
        public static void ShowWindow()
        {
            WindowRecipesRedactor window = EditorWindow.GetWindow<WindowRecipesRedactor>(false, "Recipes");
            window.minSize = new Vector2(600.0f, 320.0f);
        }

        private new void OnEnable()
        {
            base.OnEnable();
            string path = PlayerPrefs.GetString("RedactorRecipesList");
            objectList = AssetDatabase.LoadAssetAtPath<RecipesList>(path);

            path = PlayerPrefs.GetString("RedactorItemsList");
            items = AssetDatabase.LoadAssetAtPath<ItemsList>(path);
        }
        protected override void CreateObject()
        {
            Recipe recipe = new Recipe();
            SelectObject(recipe);
            objectList.Add(recipe);
        }

        protected override void DrawObject(object obj)
        {
            if (items == null) return;
            Recipe recipe = obj as Recipe;
            if (recipe == null) return;

            Item item = items.GetItem(recipe.result);
            if(item != null)
            GUILayout.Label(item.texture, GUILayout.Width(90), GUILayout.Height(90));

            GUILayout.Label("ID: " + recipe.id);
            if (item != null)
                GUILayout.Label("Имя: " + item.nameItem);
            GUILayout.EndArea();
        }

        protected override void EditableObject()
        {
            objectList = EditorGUILayout.ObjectField(objectList, typeof(RecipesList), false) as ObjectList;
            items = EditorGUILayout.ObjectField(items, typeof(ItemsList), false) as ItemsList;
        }

        protected override object GetSelectObject()
        {
            if (selectEditor == null) return null;
            return selectEditor.GetSelectObject();
        }

        protected override void RemoveSelectObject()
        {
            if (selectEditor != null && selectEditor.GetSelectObject() != null)
            {
                objectList.Remove(selectEditor.GetSelectObject());
            }
        }

        protected override void Save()
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(objectList as RecipesList);
            AssetDatabase.SaveAssets();
            RecipesExport.Export(objectList as RecipesList);
            EditorUtility.DisplayDialog("Сохранение Рецептов", "Рецеты сохранены в контейнер: " + AssetDatabase.GetAssetPath(objectList) +
                "\nФайл для экспорта на сервер: " + "Export/recipes.dat", "OK");

            PlayerPrefs.SetString("RedactorRecipesList", AssetDatabase.GetAssetPath(objectList));
            PlayerPrefs.SetString("RedactorItemsList", AssetDatabase.GetAssetPath(items));
        }

        protected override void SelectObject(object obj)
        {
            if (selectEditor == null) selectEditor = ScriptableObject.CreateInstance<RecipeEditor>();
            selectEditor.itemsList = items;
            selectEditor.Select(obj);
            Selection.activeObject = selectEditor;
            selectEditor.Repaint();
        }
    }
}
#endif