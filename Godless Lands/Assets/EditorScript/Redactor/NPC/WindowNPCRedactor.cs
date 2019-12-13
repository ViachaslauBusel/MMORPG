#if UNITY_EDITOR
using NPCs;
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NPCRedactor
{
    public class WindowNPCRedactor : EditorWindow
    {
        private RedactorStyle redactorStyle;
        private NPCList npcList;
        private int width_item = 150;
        private int height_item = 200;

        private Texture2D item_icon;
        private NPCEditor select_editor;
        private float menu_height = 30.0f;

        private int page_cur = 0;
        private int page_all;


        private void OnEnable()
        {
            select_editor = ScriptableObject.CreateInstance<NPCEditor>();
            // titleContent.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/bag.png"); //Resources.Load("Editor/Img/bag") as Texture2D;
            redactorStyle = new RedactorStyle();
        }



        [MenuItem("Window/NPCRedactor")]
        public static void ShowWindow()
        {
            WindowNPCRedactor window = EditorWindow.GetWindow<WindowNPCRedactor>(false, "NPC");
            window.minSize = new Vector2(500.0f, 320.0f);

        }
        private void DrawMenu()
        {

            GUILayout.BeginHorizontal(redactorStyle.Menu);
            GUILayout.Space(10.0f);
            if (GUILayout.Button("", redactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                Save(npcList);

                EditorUtility.DisplayDialog("Сохранение предметов", "Предметы сохранены в контейнер: " + AssetDatabase.GetAssetPath(npcList), "OK");
            }
            GUILayout.Space(20.0f);
            if (GUILayout.Button("", redactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (select_editor.npc != null && select_editor.npc.id != 0)
                {
                    if (EditorUtility.DisplayDialog("Удаление предмета", "Удалить предмет ID: " + select_editor.npc.id + ", Имя: " + select_editor.npc.name + "?", "Да", "Нет"))
                    {
                        npcList.RemoveItem(select_editor.npc);
                        select_editor.npc = null;
                        Selection.activeObject = null;
                    }
                }
                else EditorUtility.DisplayDialog("Удаление предмета", "Предмет не выбран", "OK");
            }
            GUILayout.Space(40.0f);
            npcList = EditorGUILayout.ObjectField(npcList, typeof(NPCList), false) as NPCList;
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
            if (npcList == null) return;
            //  Debug.Log(position.width);
            float height = position.height - menu_height;

            int horizontal_element = (int)position.width / width_item;
            if (horizontal_element <= 0) horizontal_element = 1;
            int verctical_element = (int)height / height_item;
            if (verctical_element <= 0) verctical_element = 1;

            float horizontal_space = (position.width - (horizontal_element * width_item)) / horizontal_element > 1 ? (horizontal_element - 1) : 1;
            float vertical_space = (height - (verctical_element * height_item)) / verctical_element > 1 ? (verctical_element - 1) : 1;

            int elementOnPage = horizontal_element * verctical_element;

            page_all = (npcList.Count + 1) / (elementOnPage);
            if (page_cur > page_all) page_cur = page_all;


            int index = page_cur * elementOnPage;
            int ver_i = 0;
            int hor_i = 0;
            for (ver_i = 0; ver_i < verctical_element; ver_i++)
            {
                for (hor_i = 0; hor_i < horizontal_element; hor_i++)
                {
                    if (npcList.Count <= index) goto off;
                    NPCPrefab npc = npcList[index++];
                    DrawItemArea(npc, ver_i, hor_i, horizontal_space, vertical_space);  //Draw Item Area
                }
            }
            off:

            if (index % elementOnPage < elementOnPage)  //если страница заполнена не польностью
            {
                if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i + 18,
                                        height_item * ver_i + vertical_space * ver_i + 38 + menu_height,   //Отрисовать кнопку добавить
                                                     64, 64), "", redactorStyle.Plus))
                {
                    NPCs.NPCPrefab _npc = new NPCs.NPCPrefab();
                    SelectItem(_npc);
                    npcList.Add(_npc);

                    //   Save(itemsList);
                }
            }
        }

        private void SelectItem(NPCs.NPCPrefab _npc) //Отоброзить в инспекторе
        {
            if (select_editor == null) select_editor = ScriptableObject.CreateInstance<NPCEditor>();
            select_editor.npc = _npc;
            Selection.activeObject = select_editor;
        }

        private void DrawItemArea(NPCs.NPCPrefab npc, int ver_i, int hor_i, float horizontal_space, float vertical_space)
        {
            GUILayout.BeginArea(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                         height_item * ver_i + vertical_space * ver_i + menu_height,
                                                     width_item, height_item),
                                                 (select_editor.npc == npc) ? redactorStyle.BackgraundSelectItem : redactorStyle.BackgraundItem); //Если предмет в инспекторе


            Texture2D texture = AssetPreview.GetAssetPreview(npc.prefab);
            TextureUtil.SetTransparent(texture);
            GUILayout.Label(texture, GUILayout.Width(130), GUILayout.Height(130));

            GUILayout.Label("ID: " + npc.id);
            GUILayout.Label("Имя: " + npc.name);
            GUILayout.EndArea();

            if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                    height_item * ver_i + vertical_space * ver_i + menu_height,
                                                    width_item, height_item), "", redactorStyle.Hide))
            {
                SelectItem(npc);
            }

        }

        public static void Save(NPCList _npcList)
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(_npcList);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif