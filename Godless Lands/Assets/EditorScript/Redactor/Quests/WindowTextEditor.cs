#if UNITY_EDITOR
using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class WindowTextEditor : EditorWindow
    {
        private static  QuestStage questEdit;

        public static void ShowWindow(QuestStage quest)
        {
            questEdit = quest;
            WindowTextEditor window = EditorWindow.GetWindow<WindowTextEditor>(false, "Text Editor");
            window.minSize = new Vector2(350.0f, 200.0f);
        }
        protected void OnEnable()
        {
            titleContent.image = Resources.Load("Editor/Img/bag") as Texture2D;
        }

        public virtual void OnGUI()
        {
            questEdit.title = GUILayout.TextField(questEdit.title);
            GUILayout.Space(20.0f);
            questEdit.descripton = GUILayout.TextField(questEdit.descripton, GUILayout.Height(150));
        }
    }
}
#endif