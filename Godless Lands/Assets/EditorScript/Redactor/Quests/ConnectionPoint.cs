#if UNITY_EDITOR
using Quests;
using System;
using UnityEngine;

namespace QuestsRedactor
{
    [System.Serializable]
    public enum ConnectionPointType { In, Out }
    [System.Serializable]
    public enum ConnectionDirection { Left, Right }

    [System.Serializable]
    public class ConnectionPoint
    {

        public ConnectionPointType type;
        public ConnectionDirection direction;

        private Vector2 _position;


        public Vector2 Position { get { return _position; } }
        public Answer Answer { get; private set; }
        public QuestStage QuestStage { get; private set; }

        public ConnectionPoint(Answer answer, ConnectionPointType type, ConnectionDirection direction)
        {
            Answer = answer;
            this.type = type;
            this.direction = direction;
        }
        public ConnectionPoint(QuestStage stage, ConnectionPointType type, ConnectionDirection direction)
        {
            QuestStage = stage;
            this.type = type;
            this.direction = direction;
        }
        public ConnectionPoint(ConnectionPointType type, ConnectionDirection direction)
        {
            this.type = type;
   
            this.direction = direction;
        }

        public void Draw()
        {


            if (GUILayout.Button("", QuestStyle.InPoint, GUILayout.Width(15), GUILayout.Height(15)))
            {
                switch (type)
                {
                    case ConnectionPointType.In:
                        WindowConnections.OnClickInPoint(this);
                        break;
                    case ConnectionPointType.Out:
                        WindowConnections.OnClickOutPoint(this);
                        break;
                }
            }
           Rect rect = GUILayoutUtility.GetLastRect();
            _position = new Vector2(QuestStage.Active.rect.x + rect.center.x, QuestStage.Active.rect.y + rect.center.y);
        }
    }
}
#endif