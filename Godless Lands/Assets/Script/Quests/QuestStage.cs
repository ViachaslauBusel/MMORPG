using QuestsRedactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class QuestStage
    {
        public Rect rect;
        public string title;
        public bool isDragged;
        public bool isSelected;

        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public GUIStyle style;
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;


        public QuestStage(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint)
        {
            rect = new Rect(position.x, position.y, width, height);
            style = nodeStyle;

            inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
            outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);

            defaultNodeStyle = nodeStyle;
            selectedNodeStyle = selectedStyle;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void Draw()
        {
            inPoint.Draw();
            outPoint.Draw();
            GUI.Box(rect, title, style);
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
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
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