#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ObjectRegistryEditor
{
    public static class Grid
    {

        /// <summary>
        /// Отрисовать сетку в пределах указанного размера
        /// </summary>
        /// <param name="window"></param>
        public static void Draw(Vector2 size)
        {
            //Маленькая сетка
            Grid.Draw(20, 0.2f, Color.gray, size);
            //Большая сетка
            Grid.Draw(100, 0.4f, Color.gray, size);
        }


        /// <summary>
        /// Отрисовать сетку в пределах указанного окна
        /// </summary>
        /// <param name="gridSpacing">Расстояние в пикселях между линиями</param>
        /// <param name="gridOpacity">Толщина линий</param>
        /// <param name="gridColor">Цвет линий</param>
        /// <param name="window">Окно в пределах которого надо отрисовать линии</param>
        public static void Draw(float gridSpacing, float gridOpacity, Color gridColor, Vector2 size)
        {
            //количество линий по горизонтали
            int verticalLines = Mathf.CeilToInt(size.x / gridSpacing);
            //количество линий по вертикали
            int horizontalLines = Mathf.CeilToInt(size.y / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);


            //Отрисовка линий по горизонтали 
            for (int i = 0; i < horizontalLines; i++)
            {
                //  Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, ZoomArea.Height(scale), 0f) + newOffset);
                float y = gridSpacing * i;
                Handles.DrawLine(new Vector3(0.0f, y), new Vector3(size.x, y));
            }
            //Отрисовка линий по вертикали
            for (int j = 0; j < verticalLines; j++)
            {
                //  Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(ZoomArea.Width(scale), gridSpacing * j, 0f) + newOffset);
                float x = gridSpacing * j;
                Handles.DrawLine(new Vector3(x, 0.0f), new Vector3(x, size.y));
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        /*  public static void DrawStages()
          {

              if (selectQuest == null || selectQuest.stages == null) return;
              for (int i = selectQuest.stages.Count - 1; i >= 0; i--)
              {
                  selectQuest.stages[i].Draw();
              }
          }*/
        /*   private static GUIStyle outPointStyle;







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
               if (GUI.Button(new Rect(5, 5, 30, 30), EditorGUIUtility.IconContent("d_tab_prev@2x"), EditorStyles.miniButtonLeft))
                   editMode = false;
           }
        */
        /*
          
           public static void DeletStage(QuestStage stage)
           {
               selectQuest.stages.Remove(stage);
           }


           public static void DrawConnections()
           {
               foreach (QuestStage stage in selectQuest.stages)
               {
                   foreach (Answer answer in stage.answers)
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
                  }
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
                     }
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

           }*/

    }
}
#endif