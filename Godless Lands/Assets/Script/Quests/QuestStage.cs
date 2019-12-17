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
        public string title = "test";
        public string descripton = "Кроме главного редактора в котором создаются квесты, имеются ещё и вспомогательные. Это редактор диалогов для персонажа и редактор сообщений для триггера. Вот описание нод используемых в данных редакторах. Поскольку они сходны я приведу описание для них обоих.";

        public bool isDragged;
        public bool isSelected;

        public ConnectionPoint inLeft, inRight;
        //   public ConnectionPoint outPoint;
        private GUIStyle style;


        public QuestStage(Vector2 position, float width, float height, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint)
        {
            rect = new Rect(position.x, position.y, width, height);

            inLeft = new ConnectionPoint(ConnectionPointType.In, ConnectionDirection.Left, OnClickInPoint);
            inRight = new ConnectionPoint(ConnectionPointType.In, ConnectionDirection.Right, OnClickInPoint);
            //  outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);

            style = QuestStyle.StageBody;
        }

        public void Load(Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint)
        {
            inLeft.OnClickConnectionPoint = OnClickInPoint;

            inRight.OnClickConnectionPoint = OnClickInPoint;
            style = QuestStyle.StageBody;
            //   outPoint.node = this;
            //  outPoint.OnClickConnectionPoint = OnClickOutPoint;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        private void DrawMenu()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(12.0f);
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_editicon.sml"), EditorStyles.miniButtonMid, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
                WindowTextEditor.ShowWindow(this);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public void Draw()
        {

            //  outPoint.Draw();
            Rect rectTitle = new Rect(rect.x, rect.y, rect.width, 40.0f);
         //   Rect rectDescription = new Rect(rect.x+100, rect.y + 45.0f, rect.width, 100.0f);
            GUILayout.BeginArea(rect, QuestStyle.StageBody);
            {
                GUILayout.Space(40.0f);
                TextWrapper.Label(descripton, 200.0f, 5);
                DrawMenu();
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(rectTitle, QuestStyle.StageTitle);
            {
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                inLeft.Draw(rectTitle);
                GUILayout.FlexibleSpace();
                GUILayout.Label(title);
                GUILayout.FlexibleSpace();
                inRight.Draw(rectTitle);
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
                            style = QuestStyle.SelectedStage;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = QuestStyle.StageBody;
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