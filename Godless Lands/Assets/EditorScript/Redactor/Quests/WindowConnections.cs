#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
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
           WindowEditMode.selectQuest.connections.Remove(connection);
        }

        private static void CreateConnection()
        {
            if (WindowEditMode.selectQuest.connections == null)
            {
                WindowEditMode.selectQuest.connections = new List<Connection>();
            }

            WindowEditMode.selectQuest.connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
        }

        private static void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }
    }
}
#endif