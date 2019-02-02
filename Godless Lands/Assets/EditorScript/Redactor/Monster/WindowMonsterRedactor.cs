#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MonsterRedactor
{
    public class WindowMonsterRedactor : EditorWindow
    {

        private RedactorStyle redactorStyle;
        private MonstersList monstersList;
        private int width_item = 150;
        private int height_item = 200;

        private Texture2D item_icon;
        private MonsterEditor select_editor;
        private float menu_height = 30.0f;

        private int page_cur = 0;
        private int page_all;


        private void OnEnable()
        {
            select_editor = ScriptableObject.CreateInstance<MonsterEditor>();
           // titleContent.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/bag.png"); //Resources.Load("Editor/Img/bag") as Texture2D;
            redactorStyle = new RedactorStyle();
        }

      

        [MenuItem("Window/MonsterRedactor")]
        public static void ShowWindow()
        {
            WindowMonsterRedactor window = EditorWindow.GetWindow<WindowMonsterRedactor>(false, "Monsters");
            window.minSize = new Vector2(500.0f, 320.0f);

        }
        private void DrawMenu()
        {

            GUILayout.BeginHorizontal(redactorStyle.Menu);
            GUILayout.Space(10.0f);
            if (GUILayout.Button("", redactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                Save(monstersList);

                EditorUtility.DisplayDialog("Сохранение предметов", "Предметы сохранены в контейнер: " + AssetDatabase.GetAssetPath(monstersList) , "OK");
            }
            GUILayout.Space(20.0f);
            if (GUILayout.Button("", redactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (select_editor.monster != null && select_editor.monster.id != 0)
                {
                    if (EditorUtility.DisplayDialog("Удаление предмета", "Удалить предмет ID: " + select_editor.monster.id + ", Имя: " + select_editor.monster.name + "?", "Да", "Нет"))
                    {
                        monstersList.RemoveItem(select_editor.monster);
                        select_editor.monster = null;
                        Selection.activeObject = null;
                    }
                }
                else EditorUtility.DisplayDialog("Удаление предмета", "Предмет не выбран", "OK");
            }
            GUILayout.Space(40.0f);
            monstersList = EditorGUILayout.ObjectField(monstersList, typeof(MonstersList), false) as MonstersList;
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
            if (monstersList == null) return;
            //  Debug.Log(position.width);
            float height = position.height - menu_height;

            int horizontal_element = (int)position.width / width_item;
            int verctical_element = (int)height / height_item;

            float horizontal_space = (position.width - (horizontal_element * width_item)) / (horizontal_element - 1);
            float vertical_space = (height - (verctical_element * height_item)) / (verctical_element - 1);

            int elementOnPage = horizontal_element * verctical_element;
            page_all = (monstersList.Count + 1) / (elementOnPage);
            if (page_cur > page_all) page_cur = page_all;


            int index = page_cur * elementOnPage;
            int ver_i = 0;
            int hor_i = 0;
            for (ver_i = 0; ver_i < verctical_element; ver_i++)
            {
                for (hor_i = 0; hor_i < horizontal_element; hor_i++)
                {
                    if (monstersList.Count <= index) goto off;
                    Monster monster = monstersList[index++];
                    DrawItemArea(monster, ver_i, hor_i, horizontal_space, vertical_space);  //Draw Item Area

                }
            }
            off:

            if (index % elementOnPage < elementOnPage)  //если страница заполнена не польностью
            {
                if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i + 18,
                                        height_item * ver_i + vertical_space * ver_i + 38 + menu_height,   //Отрисовать кнопку добавить
                                                     64, 64), "", redactorStyle.Plus))
                {
                    Monster _monster = new Monster();
                    SelectItem(_monster);
                    monstersList.Add(_monster);

                 //   Save(itemsList);
                }
            }
        }

        private void SelectItem(Monster _monster) //Отоброзить в инспекторе
        {
            if(select_editor == null) select_editor = ScriptableObject.CreateInstance<MonsterEditor>();
            select_editor.monster = _monster;
            Selection.activeObject = select_editor;
        }

        private void DrawItemArea(Monster monster, int ver_i, int hor_i, float horizontal_space, float vertical_space)
        {
            GUILayout.BeginArea(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                         height_item * ver_i + vertical_space * ver_i + menu_height,
                                                     width_item, height_item),
                                                 (select_editor.monster == monster) ? redactorStyle.BackgraundSelectItem : redactorStyle.BackgraundItem); //Если предмет в инспекторе


            Texture2D texture = AssetPreview.GetAssetPreview(monster.prefab);
            TextureUtil.SetTransparent(texture);
            GUILayout.Label(texture, GUILayout.Width(130), GUILayout.Height(130));
             
            GUILayout.Label("ID: " + monster.id);
            GUILayout.Label("Имя: " + monster.name);
            GUILayout.EndArea();

            if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                    height_item * ver_i + vertical_space * ver_i + menu_height,
                                                    width_item, height_item), "", redactorStyle.Hide))
            {
                SelectItem(monster);
            }

        }

        public static void Save(MonstersList monsterList)
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(monsterList);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif