#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{

    public class Connection
    {
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;
        public Action<Connection> OnClickRemoveConnection;

        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
        }

        public void Draw()
        {
            Handles.DrawBezier(
                inPoint.Position,
                outPoint.Position,
                inPoint.Position + ((inPoint.direction == ConnectionDirection.Left)? Vector2.left: Vector2.right) * 50f,
                outPoint.Position + ((outPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
                Color.white,
                null,
                2f
            );

          /*  if (Handles.Button((inPoint.Position + outPoint.Position) * 0.5f, Quaternion.identity, 4, 8, Handles.CapFunction))
            {
                if (OnClickRemoveConnection != null)
                {
                    OnClickRemoveConnection(this);
                }
            }*/
        }
    }
}
#endif