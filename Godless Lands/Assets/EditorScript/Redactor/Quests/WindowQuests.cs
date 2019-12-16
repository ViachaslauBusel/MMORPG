#if UNITY_EDITOR

using Quests;
using Recipes;
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class WindowQuests : Window
    {
        //  private RecipeEditor selectEditor;
        private Quest selectQuest;
        private bool editMode = false;
   

        [MenuItem("Window/Quests")]
        public static void ShowWindow()
        {
            WindowQuests window = EditorWindow.GetWindow<WindowQuests>(false, "Quests");
            window.minSize = new Vector2(600.0f, 320.0f);
        }

        private new void OnEnable()
        {
            base.OnEnable();
            string path = PlayerPrefs.GetString("RedactorQuestsList");
            objectList = AssetDatabase.LoadAssetAtPath<QuestsList>(path);

            WindowEditMode.OnEnable();
        }
        public override void OnGUI()
        {
            if (editMode)
            {
                WindowEditMode.DrawGrid(20, 0.2f, Color.gray);
                WindowEditMode.DrawGrid(100, 0.4f, Color.gray);
                WindowEditMode.DrawMenu(ref editMode);
                WindowEditMode.DrawStages(selectQuest);
                WindowEditMode.DrawConnections();
                WindowEditMode.DrawConnectionLine(Event.current);
                WindowEditMode.ProcessStageEvents(Event.current);
                WindowEditMode.ProcessEvents(Event.current);
              
            }
            else base.OnGUI();
        }
        protected override void CreateObject()
        {
            Quest _quest = new Quest();
            SelectObject(_quest);
            objectList.Add(_quest);
        }

        protected override void DrawObject(object obj)
        {
            Quest _quest = obj as Quest;
            if (_quest == null) return;
            // GUILayout.BeginArea();
            //  Item item = quests.GetItem(_quest.result);
            //  if (item != null)
            //     GUILayout.Label(item.texture, GUILayout.Width(90), GUILayout.Height(90));
            GUILayout.BeginVertical();
            {
                GUILayout.Label("ID: " + _quest.id);
                //    if (item != null)
                GUILayout.Label("Имя: " + _quest.title);
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button(EditorGUIUtility.IconContent("d_editicon.sml"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
                    {
                        selectQuest = _quest;
                        editMode = true;
                    }
                } GUILayout.EndHorizontal();
            } GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        protected override void EditableObject()
        {
            objectList = EditorGUILayout.ObjectField(objectList, typeof(QuestsList), false) as ObjectList;
        }

        protected override object GetSelectObject()
        {
            //     if (selectEditor == null) return null;
         //   if (selectQuest < 0 || objectList.Count >= selectQuest) return null;
            return selectQuest;// selectEditor.GetSelectObject();
        }

        protected override void RemoveSelectObject()
        {


                objectList.Remove(selectQuest);

        }

        protected override void Save()
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(objectList as QuestsList);
            AssetDatabase.SaveAssets();
            QuestsExport.Export(objectList as QuestsList);
            EditorUtility.DisplayDialog("Сохранение Рецептов", "Рецеты сохранены в контейнер: " + AssetDatabase.GetAssetPath(objectList) +
                "\nФайл для экспорта на сервер: " + "Export/quests.dat", "OK");

            PlayerPrefs.SetString("RedactorQuestsList", AssetDatabase.GetAssetPath(objectList));
        }

        protected override void SelectObject(object obj)
        {
            Quest _quest = obj as Quest;
            if (_quest == null) return;
            selectQuest = _quest;
            /*   if (selectEditor == null) selectEditor = ScriptableObject.CreateInstance<RecipeEditor>();
                selectEditor.itemsList = quests;
                selectEditor.Select(obj);
                Selection.activeObject = selectEditor;
                selectEditor.Repaint();*/
        }

    }
}
#endif