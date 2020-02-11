#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    [System.Serializable]
    public class Answer
    {
        public ConnectionPoint inLeft, inRight;
        public int id;


        public Answer()
        {
            inLeft = new ConnectionPoint(ConnectionPointType.Out, ConnectionDirection.Left);
            inRight = new ConnectionPoint(ConnectionPointType.Out, ConnectionDirection.Right);
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(EditorGUIUtility.IconContent("d_P4_AddedLocal"), EditorStyles.miniButtonMid, GUILayout.Width(165.0f), GUILayout.Height(25.0f)))
            {
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
#endif