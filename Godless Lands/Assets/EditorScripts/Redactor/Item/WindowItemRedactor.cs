#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Items
{
    public class WindowItemRedactor : EditorWindow
    {

        private RedactorStyle redactorStyle;

        private ItemsList itemsList;
        private int width_item = 100;
        private int height_item = 140;

  
        private ItemEditor select_editor;
        private float menu_height = 30.0f;

        private int page_cur = 0;
        private int page_all;

        [MenuItem("Window/Items")]
        public static void ShowWindow()
        {
            WindowItemRedactor window = EditorWindow.GetWindow<WindowItemRedactor>(false, "Items");
            window.minSize = new Vector2(600.0f, 320.0f);

        }


        private void OnEnable()
        {
            select_editor = ScriptableObject.CreateInstance<ItemEditor>();
            titleContent.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/bag.png"); //Resources.Load("Editor/Img/bag") as Texture2D;
            redactorStyle = new RedactorStyle();

            string path = PlayerPrefs.GetString("WindowItemRedactorItemsList");
            itemsList = AssetDatabase.LoadAssetAtPath<ItemsList>(path);
        }

        private void DrawMenu()
        {

            GUILayout.BeginHorizontal(redactorStyle.Menu);
            GUILayout.Space(10.0f);
            if (GUILayout.Button("", redactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                Save(itemsList);
                ItemsExport.Export(itemsList.GetList());
                EditorUtility.DisplayDialog("Сохранение предметов", "Предметы сохранены в контейнер: " + AssetDatabase.GetAssetPath(itemsList) +
                    "\nФайл для экспорта на сервер: " + "Export/items.dat", "OK");
            }
            GUILayout.Space(20.0f);
            if (GUILayout.Button("", redactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (select_editor._item != null && select_editor._item.id != 0)
                {
                    if (EditorUtility.DisplayDialog("Удаление предмета", "Удалить предмет ID: " + select_editor._item.id + ", Имя: " + select_editor._item.nameItem + "?", "Да", "Нет"))
                    {
                        itemsList.RemoveItem(select_editor._item);
                        select_editor._item = null;
                        Selection.activeObject = null;
                    }
                }
                else EditorUtility.DisplayDialog("Удаление предмета", "Предмет не выбран", "OK");
            }
            GUILayout.Space(40.0f);
            itemsList = EditorGUILayout.ObjectField(itemsList, typeof(ItemsList), false) as ItemsList;
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("", redactorStyle.Left, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (page_cur > 0) page_cur--;
            }
            GUILayout.Space(5.0f);
            GUILayout.Label((page_cur + 1) + "/" + (page_all + 1), redactorStyle.Text);
            GUILayout.Space(5.0f);
            if (GUILayout.Button("", redactorStyle.Right, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (page_cur < page_all) page_cur++;
            }
            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }

        private void OnGUI()
        {
            DrawMenu();
            if (itemsList == null) return;
            //  Debug.Log(position.width);
            float height = position.height - menu_height;

            int horizontal_element = (int)position.width / width_item;
            int verctical_element = (int)height / height_item;

            float horizontal_space = (position.width - (horizontal_element * width_item)) / (horizontal_element - 1);
            float vertical_space = (height - (verctical_element * height_item)) / (verctical_element - 1);

            int elementOnPage = horizontal_element * verctical_element;
            page_all = (itemsList.Count + 1) / (elementOnPage);
            if (page_cur > page_all) page_cur = page_all;


            int index = page_cur * elementOnPage;
            int ver_i = 0;
            int hor_i = 0;
            for (ver_i = 0; ver_i < verctical_element; ver_i++)
            {
                for (hor_i = 0; hor_i < horizontal_element; hor_i++)
                {
                    if (itemsList.Count <= index) goto off;
                    Item _item = itemsList[index++];
                    DrawItemArea(_item, ver_i, hor_i, horizontal_space, vertical_space);

                }
            }
            off:

            if (index % elementOnPage < elementOnPage)
            {
                if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i + 18,
                                        height_item * ver_i + vertical_space * ver_i + 38 + menu_height,
                                                     64, 64), "", redactorStyle.Plus))
                {
                    Item _item = new Item();
                    SelectItem(_item);
                    itemsList.AddItem(_item);

                    Save(itemsList);
                }
            }
        }


        private void SelectItem(Item _item)
        {
            if(select_editor == null) select_editor = ScriptableObject.CreateInstance<ItemEditor>();
            select_editor.Select(_item);
            Selection.activeObject = select_editor;
        }

        private void DrawItemArea(Item _item, int ver_i, int hor_i, float horizontal_space, float vertical_space)
        {
            GUILayout.BeginArea(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                         height_item * ver_i + vertical_space * ver_i + menu_height,
                                                     width_item, height_item),
                                                 (select_editor._item == _item) ? redactorStyle.BackgraundSelectItem : redactorStyle.BackgraundItem);

            GUILayout.Label(_item.texture, GUILayout.Width(90), GUILayout.Height(90));
            GUILayout.Label("ID: " + _item.id);
            GUILayout.Label("Имя: " + _item.nameItem);
            GUILayout.EndArea();

            if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                    height_item * ver_i + vertical_space * ver_i + menu_height,
                                                    width_item, height_item), "", redactorStyle.Hide))
            {
                SelectItem(_item);
            }

        }

        public static void Save(ItemsList itemsList)
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(itemsList);
            AssetDatabase.SaveAssets();

            PlayerPrefs.SetString("WindowItemRedactorItemsList", AssetDatabase.GetAssetPath(itemsList));
        }
        public static ItemsList Create()
        {
            ItemsList asset = ScriptableObject.CreateInstance<ItemsList>();

            AssetDatabase.CreateAsset(asset, "Assets/Prefab/Inventory/ItemList.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }

    }
}
#endif