#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ObjectRegistryEditor.Tools
{
    public static class Box
    {

        private static Stack<Action> m_endDraw = new Stack<Action>();
        public static void BeginDraw(Color background, Color border, int borderDepth, string title, Action close)
        {
            Action endClose = null;


            EditorGUILayout.BeginVertical(BackgroundStyle.Get(border));
            EditorGUILayout.BeginVertical(BoxStyle.Get(background, 2));
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(title);
            GUILayout.FlexibleSpace();

           
            if (close != null)
            {
                if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(25))) { endClose = close; }
            }
            EditorGUILayout.EndHorizontal();

            m_endDraw.Push(() =>
            {
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();
                endClose?.Invoke();
            });
        }

        public static void BeginDraw(Color background, string title)
        {
            EditorGUILayout.BeginVertical(BackgroundStyle.Get(background));
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(title);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            m_endDraw.Push(() =>
            {
                EditorGUILayout.EndVertical();
            });
        }

        public static void EndDraw()
        {
            m_endDraw.Pop()?.Invoke();
        }
    }
}
#endif