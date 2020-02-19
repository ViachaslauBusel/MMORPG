#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Redactor
{
    public class WindowGrid
    {
        private static Vector2 offset = Vector2.zero;

        public static void Draw(Vector2 drag, float gridSpacing, float gridOpacity, Color gridColor, float scale = 1.0f)
        {
            int widthDivs = Mathf.CeilToInt(EditorZoomArea.Width(scale) / gridSpacing);
            int heightDivs = Mathf.CeilToInt(EditorZoomArea.Height(scale) / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
            {
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, EditorZoomArea.Height(scale), 0f) + newOffset);
            }

            for (int j = 0; j < heightDivs; j++)
            {
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(EditorZoomArea.Width(scale), gridSpacing * j, 0f) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}
#endif