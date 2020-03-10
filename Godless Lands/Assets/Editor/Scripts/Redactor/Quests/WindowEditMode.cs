#if UNITY_EDITOR
using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class WindowEditMode
    {
        public static Quest selectQuest;
        private static bool editMode = false;





        private static GUIStyle outPointStyle;





        private static Vector2 drag;

        public static void OnEnable()
        {
            
          

         

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn up.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn up on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);



        }

        public static bool IsActivated() { return editMode; }
        public static void Activate(Quest _quest)
        {
            selectQuest = _quest;
            foreach (QuestStage stage in _quest.stages)
                stage.Load(); 

            editMode = true;
        }
        public static void Deactivate()
        {
            editMode = false;
            selectQuest = null;
        }

        /// <summary>
        /// Отрисовывает сетку и кнопку возврата
        /// </summary>
        public static void DrawMenu()
        {
            
            //Кнопка возврата. верхний левый угол
            if (GUI.Button(new Rect(5,5,30,30), EditorGUIUtility.IconContent("d_tab_prev@2x"), EditorStyles.miniButtonLeft))
                editMode = false;
        }

        public static void DrawGrid(float scale)
        {
            //Маленькая сетка
            WindowGrid.Draw(drag, 20, 0.2f, Color.gray, scale);
            //Большая сетка
            WindowGrid.Draw(drag, 100, 0.4f, Color.gray, scale);
        }
        public static void DrawStages()
        {
           
            if (selectQuest == null || selectQuest.stages == null) return;
            for(int i= selectQuest.stages.Count-1; i >= 0 ; i--)
            {
                selectQuest.stages[i].Draw();
            }
        }
        public static void DeletStage(QuestStage stage)
        {
            selectQuest.stages.Remove(stage);
        }


        public static void DrawConnections()
        {
            foreach(QuestStage stage in selectQuest.stages)
            {
                foreach(Answer answer in stage.answers)
                {
                    answer.DrawBezier();
                }
            }
         /*   if (selectQuest.connections != null)
            {
                for (int i = 0; i < selectQuest.connections.Count; i++)
                {
                    selectQuest.connections[i].Draw();
                }
            }*/
        }

        public static void DrawConnectionLine(Event e)
        {
      /*      if (selectedInPoint != null && selectedOutPoint == null)//Рисование линии от нажатой аут к мыши
            {
                Handles.DrawBezier(
                    selectedInPoint.Position,
                    e.mousePosition,
                    selectedInPoint.Position + ((selectedInPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
                    e.mousePosition - ((selectedInPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.Position,
                    e.mousePosition,
                    selectedOutPoint.Position + ((selectedOutPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
                    e.mousePosition - ((selectedOutPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }*/
        }

        public static void ProcessEvents(Event e)
        {
            drag = Vector2.zero;
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        OnDrag(e.delta);
                    }
                    break;
            }
        }

        public static void OnDrag(Vector2 delta)
        {

            drag = delta;

            if (selectQuest.stages != null)
            {
                for (int i = 0; i < selectQuest.stages.Count; i++)
                {
                    selectQuest.stages[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }
      //  private static bool guiChanged = false;
        public static void ProcessStageEvents(Event e)
        {
          //  guiChanged = false;
            if (selectQuest.stages != null)
            {
                for (int i = selectQuest.stages.Count - 1; i >= 0; i--)
                {
                 bool mark = selectQuest.stages[i].ProcessEvents(e);

                    if (mark)
                    {
                     mark = GUI.changed = true;
                    }
                }
            }
        }

        private static void ProcessContextMenu(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add stage"), false, () => OnClickAddNode(mousePosition));
            genericMenu.ShowAsContext();
        }

       
        private static void OnClickAddNode(Vector2 mousePosition)
        {
            if (selectQuest.stages == null)
            {
                selectQuest.stages = new List<QuestStage>();
            }

            int idStages = 0;
            while (selectQuest.Contains(idStages))
            { idStages++; }

            selectQuest.stages.Add(new QuestStage(mousePosition, 200, 200, idStages));

        }

      
    }
}
#endif