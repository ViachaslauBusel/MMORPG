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

        public Action<ConnectionPoint> OnClickConnectionPoint;

        public Vector2 Position { get { return _position; } }

        public ConnectionPoint(ConnectionPointType type, ConnectionDirection direction, Action<ConnectionPoint> OnClickConnectionPoint)
        {
            this.type = type;
            this.OnClickConnectionPoint = OnClickConnectionPoint;
            this.direction = direction;
        }

        public void Draw(Rect lastRect)
        {


            if (GUILayout.Button("", QuestStyle.InPoint, GUILayout.Width(15), GUILayout.Height(15)))
            {
                if (OnClickConnectionPoint != null)
                {
                    OnClickConnectionPoint(this);
                }
            }
           Rect rect = GUILayoutUtility.GetLastRect();
            _position = new Vector2(lastRect.x + rect.center.x, lastRect.y + rect.center.y);
        }
    }
}
#endif