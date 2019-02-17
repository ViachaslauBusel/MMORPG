#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Redactor
{
    public abstract class Window: EditorWindow
    {
        private RedactorStyle redactorStyle;
        private int width_item = 100;
        private int height_item = 140;

        private Texture2D item_icon;
        private float menu_height = 30.0f;

        private int page_cur = 0;
        private int page_all;

        protected ObjectList objectList;

        protected void OnEnable()
        {
            titleContent.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/bag.png"); //Resources.Load("Editor/Img/bag") as Texture2D;
            redactorStyle = new RedactorStyle();
        }

        private void OnGUI()
        {
            DrawMenu();
            if (objectList == null) return;
            DrawWindow();
        }

        protected void DrawMenu()
        {

            GUILayout.BeginHorizontal(redactorStyle.Menu);
            GUILayout.Space(10.0f);
            if (GUILayout.Button("", redactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                Save();
            }
            GUILayout.Space(20.0f);
            if (GUILayout.Button("", redactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            {
                RemoveSelectObject();
            }
            GUILayout.Space(40.0f);
            EditableObject();//Выбор редактироваемого ассета
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

        protected void DrawWindow()
        {
            float height = position.height - menu_height;

            int horizontal_element = (int)position.width / width_item;
            int verctical_element = (int)height / height_item;

            float horizontal_space = (position.width - (horizontal_element * width_item)) / (horizontal_element - 1);
            float vertical_space = (height - (verctical_element * height_item)) / (verctical_element - 1);

            int elementOnPage = horizontal_element * verctical_element;
            page_all = (objectList.Count + 1) / (elementOnPage);
            if (page_cur > page_all) page_cur = page_all;


            int index = page_cur * elementOnPage;
            int ver_i = 0;
            int hor_i = 0;
            for (ver_i = 0; ver_i < verctical_element; ver_i++)
            {
                for (hor_i = 0; hor_i < horizontal_element; hor_i++)
                {
                    if (objectList.Count <= index) goto off;
                    DrawItemArea(objectList[index++], ver_i, hor_i, horizontal_space, vertical_space);

                }
            }
            off:

            if (index % elementOnPage < elementOnPage)
            {
                if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i + 18,
                                        height_item * ver_i + vertical_space * ver_i + 38 + menu_height,
                                                     64, 64), "", redactorStyle.Plus))
                {
                    CreateObject();
                }
            }
        }

        private void DrawItemArea(System.Object obj, int ver_i, int hor_i, float horizontal_space, float vertical_space)
        {
            GUILayout.BeginArea(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                         height_item * ver_i + vertical_space * ver_i + menu_height,
                                                     width_item, height_item),
                                                 (obj.Equals(GetSelectObject())) ? redactorStyle.BackgraundSelectItem : redactorStyle.BackgraundItem);

            DrawObject(obj);

            if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                    height_item * ver_i + vertical_space * ver_i + menu_height,
                                                    width_item, height_item), "", redactorStyle.Hide))
            {
                SelectObject(obj);
            }

        }

        protected abstract void Save();
        protected abstract void CreateObject();//Создание нового предмета
        protected abstract void SelectObject(System.Object obj);
        protected abstract void DrawObject(System.Object obj);
        protected abstract void RemoveSelectObject();
        protected abstract void EditableObject();//Выбор редактируемого ассета
        protected abstract System.Object GetSelectObject();
    }
}
#endif