#if UNITY_EDITOR
using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class WindowEditMode
    {
        private static Quest selectQuest;


        private static List<Connection> connections;

        private static GUIStyle nodeStyle;
        private static GUIStyle inPointStyle;
        private static GUIStyle outPointStyle;
        private static GUIStyle selectedNodeStyle;

        private static ConnectionPoint selectedInPoint;
        private static ConnectionPoint selectedOutPoint;

        private static Vector2 offset;
        private static Vector2 drag;

        public static void OnEnable()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.border = new RectOffset(4, 4, 12, 12);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        }

        public static void DrawMenu(ref bool editMode)
        {
            if (GUI.Button(new Rect(5,5,30,30), EditorGUIUtility.IconContent("vcs_refresh"), EditorStyles.miniButtonLeft))
            {
                editMode = false;
            }
        }
        public static void DrawStages(Quest _quest)
        {
            selectQuest = _quest;
            if (selectQuest == null || selectQuest.stages == null) return;
            foreach(QuestStage stage in selectQuest.stages)
            {
                stage.Draw();
            }
        }

        public static void DrawConnections()
        {
            if (connections != null)
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    connections[i].Draw();
                }
            }
        }

        public static void DrawConnectionLine(Event e)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );

                GUI.changed = true;
            }

            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
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

        private static void OnDrag(Vector2 delta)
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
        private static bool guiChanged = false;
        public static void ProcessStageEvents(Event e)
        {
            guiChanged = false;
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

        public static void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(Screen.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(Screen.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, Screen.height, 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(Screen.width, gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
        private static void OnClickAddNode(Vector2 mousePosition)
        {
            if (selectQuest.stages == null)
            {
                selectQuest.stages = new List<QuestStage>();
            }

            selectQuest.stages.Add(new QuestStage(mousePosition, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint));

        }

        private static void OnClickInPoint(ConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private static void OnClickOutPoint(ConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private static void OnClickRemoveConnection(Connection connection)
        {
            connections.Remove(connection);
        }

        private static void CreateConnection()
        {
            if (connections == null)
            {
                connections = new List<Connection>();
            }

            connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
        }

        private static void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }
    }
}
#endif