using UnityEditor;
using UnityEngine;

namespace NodeEditor
{
    public static class Grid
    {
        // <summary>
        /// Draw grid within the specified size.
        /// </summary>
        /// <param name="size">Size of the grid.</param>
        public static void DrawGrid(Vector2 size)
        {
            // Small grid
            DrawGrid(20, 0.2f, Color.gray, size);
            // Large grid
            DrawGrid(100, 0.4f, Color.gray, size);
        }

        /// <summary>
        /// Draw grid within the specified window.
        /// </summary>
        /// <param name="gridSpacing">Distance in pixels between lines.</param>
        /// <param name="gridOpacity">Thickness of lines.</param>
        /// <param name="gridColor">Color of lines.</param>
        /// <param name="size">Size of the window.</param>
        private static void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor, Vector2 size)
        {
            // Number of lines horizontally
            int verticalLines = Mathf.CeilToInt(size.x / gridSpacing);
            // Number of lines vertically
            int horizontalLines = Mathf.CeilToInt(size.y / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            // Draw horizontal lines
            for (int i = 0; i < horizontalLines; i++)
            {
                float y = gridSpacing * i;
                Handles.DrawLine(new Vector3(0.0f, y), new Vector3(size.x, y));
            }

            // Draw vertical lines
            for (int j = 0; j < verticalLines; j++)
            {
                float x = gridSpacing * j;
                Handles.DrawLine(new Vector3(x, 0.0f), new Vector3(x, size.y));
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}
