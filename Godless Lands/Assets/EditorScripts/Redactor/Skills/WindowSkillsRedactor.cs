#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SkillsRedactor
{
    public class WindowSkillsRedactor : EditorWindow
    {
        private RedactorStyle redactorStyle;
        private int page_cur = 0;
        private int page_all;
        private SkillsList skillsList;
        private float menu_height = 30.0f;
        private int width_item = 100;
        private int height_item = 140;
        private SkillEditor select_editor;

        [MenuItem("Window/SkillsRedactor")]
        public static void ShowWindow()
        {
            WindowSkillsRedactor window = EditorWindow.GetWindow<WindowSkillsRedactor>(false, "Skills");
            window.minSize = new Vector2(500.0f, 320.0f);

        }

        private void OnEnable()
        {
            select_editor = ScriptableObject.CreateInstance<SkillEditor>();
            redactorStyle = new RedactorStyle();
        }
        private void DrawMenu()
        {

            GUILayout.BeginHorizontal(redactorStyle.Menu);
            GUILayout.Space(10.0f);
            if (GUILayout.Button("", redactorStyle.SaveOnDisk, GUILayout.Height(25), GUILayout.Width(25)))
            {
                Save(skillsList);
                SkillsExport.Export(skillsList);
               EditorUtility.DisplayDialog("Сохранение предметов", "Предметы сохранены в контейнер: " + AssetDatabase.GetAssetPath(skillsList), "OK");
            }
            GUILayout.Space(20.0f);
            if (GUILayout.Button("", redactorStyle.Delet, GUILayout.Height(25), GUILayout.Width(25)))
            {
                if (select_editor.skill != null && select_editor.skill.id != 0)
                {
                    if (EditorUtility.DisplayDialog("Удаление предмета", "Удалить предмет ID: " + select_editor.skill.id + ", Имя: " + select_editor.skill.name + "?", "Да", "Нет"))
                    {
                        skillsList.RemoveItem(select_editor.skill);
                        select_editor.skill = null;
                        Selection.activeObject = null;
                    }
                }
                else EditorUtility.DisplayDialog("Удаление предмета", "Предмет не выбран", "OK");
            }
            GUILayout.Space(40.0f);
            skillsList = EditorGUILayout.ObjectField(skillsList, typeof(SkillsList), false) as SkillsList;
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
            if (skillsList == null) return;
            float height = position.height - menu_height;

            int horizontal_element = (int)position.width / width_item;
            int verctical_element = (int)height / height_item;

            float horizontal_space = (position.width - (horizontal_element * width_item)) / (horizontal_element - 1);
            float vertical_space = (height - (verctical_element * height_item)) / (verctical_element - 1);

            int elementOnPage = horizontal_element * verctical_element;
            page_all = (skillsList.Count + 1) / (elementOnPage);
            if (page_cur > page_all) page_cur = page_all;


            int index = page_cur * elementOnPage;
            int ver_i = 0;
            int hor_i = 0;
            for (ver_i = 0; ver_i < verctical_element; ver_i++)
            {
                for (hor_i = 0; hor_i < horizontal_element; hor_i++)
                {
                    if (skillsList.Count <= index) goto off;
                    Skill skill = skillsList[index++];
                    DrawItemArea(skill, ver_i, hor_i, horizontal_space, vertical_space);  //Draw Item Area

                }
            }
            off:

            if (index % elementOnPage < elementOnPage)  //если страница заполнена не польностью
            {
                if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i + 18,
                                        height_item * ver_i + vertical_space * ver_i + 38 + menu_height,   //Отрисовать кнопку добавить
                                                     64, 64), "", redactorStyle.Plus))
                {
                    Skill _skill = new Skill();
                    SelectItem(_skill);
                    skillsList.Add(_skill);

                    //   Save(itemsList);
                }
            }
        }

        private void SelectItem(Skill skill) //Отоброзить в инспекторе
        {
            if (select_editor == null) select_editor = ScriptableObject.CreateInstance<SkillEditor>();
            select_editor.skill = skill;
            select_editor.serializableObject = null;
            Selection.activeObject = select_editor;
        }

        private void DrawItemArea(Skill skill, int ver_i, int hor_i, float horizontal_space, float vertical_space)
        {
            GUILayout.BeginArea(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                         height_item * ver_i + vertical_space * ver_i + menu_height,
                                                     width_item, height_item),
                                                 (select_editor.skill == skill) ? redactorStyle.BackgraundSelectItem : redactorStyle.BackgraundItem); //Если предмет в инспекторе



            GUILayout.Label(skill.icon, GUILayout.Width(90), GUILayout.Height(90));

            GUILayout.Label("ID: " + skill.id);
            GUILayout.Label("Имя: " + skill.name);
            GUILayout.EndArea();

            if (GUI.Button(new Rect(width_item * hor_i + horizontal_space * hor_i,
                                    height_item * ver_i + vertical_space * ver_i + menu_height,
                                                    width_item, height_item), "", redactorStyle.Hide))
            {
                SelectItem(skill);
            }

        }

        public static void Save(SkillsList skillsList)
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(skillsList);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif