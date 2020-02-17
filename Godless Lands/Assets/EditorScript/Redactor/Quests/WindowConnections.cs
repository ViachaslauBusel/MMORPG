#if UNITY_EDITOR
using Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class WindowConnections
    {
        private static ConnectionPoint selectedInPoint;
        private static ConnectionPoint selectedOutPoint;

        public static void OnClickInPoint(ConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;

            if (selectedOutPoint != null)
            {
                if (selectedOutPoint != selectedInPoint)
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

        public static void OnClickOutPoint(ConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;

            if (selectedInPoint != null)
            {
                if (selectedOutPoint != selectedInPoint)
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
         //  WindowEditMode.selectQuest.connections.Remove(connection);
        }

        private static void CreateConnection()
        {
            selectedOutPoint.Answer.idStage = selectedInPoint.QuestStage.id;
        }

        private static void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }

        public static void Draw(Answer answer)
        {
            QuestStage stage = WindowQuests.Instance.selectQuest.FindStage(answer.idStage);
            if (stage == null) return;
            ConnectionPoint outPoint = answer.inRight;
            ConnectionPoint inPoint = stage.inLeft;

            if(Vector2.Distance(inPoint.Position, outPoint.Position) > Vector3.Distance(answer.inLeft.Position, stage.inRight.Position))
            {
                outPoint = answer.inLeft;
                inPoint = stage.inRight;
            }
   
            Handles.DrawBezier(
               inPoint.Position,
               outPoint.Position,
               inPoint.Position + ((inPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
               outPoint.Position + ((outPoint.direction == ConnectionDirection.Left) ? Vector2.left : Vector2.right) * 50f,
               Color.green,
               null,
               4f
           );

           /* if (Handles.Button((inPoint.Position + outPoint.Position) * 0.5f, Quaternion.identity, 6, 10, Handles.RectangleHandleCap))
            {
                Debug.Log("Click");
                answer.idStage = -1;
            }*/
        }
    }
}
#endif