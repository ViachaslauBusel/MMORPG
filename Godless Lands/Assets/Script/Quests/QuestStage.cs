using QuestsRedactor;
using Redactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class QuestStage
    {
        public Rect rect;
        //ИД этого звена(уровня)
        public int id;
        public string title = "no title";
        public string descripton = "empty";

        public bool isDragged;
        public bool isSelected;

        public ConnectionPoint inLeft, inRight;
        public List<Answer> answers;
        //   public ConnectionPoint outPoint;
        private GUIStyle style;
        /// <summary>
        /// Активный в атрисовке
        /// </summary>
        public static QuestStage Active { get; private set; }


        public QuestStage(Vector2 position, float width, float height, int idStage)
        {
            id = idStage;
            rect = new Rect(position.x, position.y, width, height);

            inLeft = new ConnectionPoint(this, ConnectionPointType.In, ConnectionDirection.Left);
            inRight = new ConnectionPoint(this, ConnectionPointType.In, ConnectionDirection.Right);
            //  outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);
            answers = new List<Answer>();
            answers.Add(new Answer());
            style = QuestStyle.StageBody;
        }

        public void Load()
        {
           // inLeft.OnClickConnectionPoint = OnClickInPoint;

          //  inRight.OnClickConnectionPoint = OnClickInPoint;
            style = QuestStyle.StageBody;
            //   outPoint.node = this;
            //  outPoint.OnClickConnectionPoint = OnClickOutPoint;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        /// <summary>
        /// Отрисовывает кнопки управления
        /// </summary>
        private void DrawMenu()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(12.0f);
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_P4_DeletedLocal"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                if(EditorUtility.DisplayDialog("delet", "Вы действительно хотите удалить звено?", "ДА", "НЕТ"))
                {
                    WindowEditMode.DeletStage(this);
                }
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_editicon.sml"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                WindowTextEditor.ShowWindow(this);
            }
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_P4_AddedRemote"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                answers.Add(new Answer());
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public void Draw()
        {
            Active = this;
            //  outPoint.Draw();
            Rect rectTitle = new Rect(rect.x, rect.y, rect.width, 40.0f);
            Rect rectBody = new Rect(rect.x, rect.y, rect.width, rect.height + answers.Count * 15.0f);


            //Тело
            GUILayout.BeginArea(rectBody, QuestStyle.StageBody);
            {
                GUILayout.Space(40.0f);
                TextWrapper.Label(descripton, 200.0f, 5);
                DrawMenu();

                //Отрисовка и удаление ответов
                for (int i = answers.Count-1; i >= 0; i--)
                { 
                    if (answers[i].Draw())
                        answers.RemoveAt(i);
                    
                }
        
            }
            GUILayout.EndArea();

            //Заголовок
            GUILayout.BeginArea(rectTitle, isSelected? QuestStyle.StageTitleSelect : QuestStyle.StageTitle);
            {
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                inLeft.Draw();
                GUILayout.FlexibleSpace();
                GUILayout.Label(title);
                GUILayout.FlexibleSpace();
                inRight.Draw();
                GUILayout.Space(15);
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
           
        }


        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                            isSelected = true;
                         //   style = QuestStyle.SelectedStage;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                           // style = QuestStyle.StageBody;
                        }
                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}