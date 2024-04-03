#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ObjectRegistryEditor.Tools
{
    public static class BackgroundStyle
    {
        private static GUIStyle style = new GUIStyle();
        private static Texture2D texture = new Texture2D(1, 1);


        public static GUIStyle Get(Color color)
        {
            if (style == null) style = new GUIStyle();
            if (texture == null) texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            style.normal.background = texture;
            return style;
        }
    }

    public static class TextStyle
    {
        private static GUIStyle style = new GUIStyle();


        public static GUIStyle Get(Color color)
        {
            style.normal.textColor = color;
            return style;
        }
    }

    public static class BoxStyle
    {
        private static GUIStyle style = new GUIStyle();
        private static Texture2D texture = new Texture2D(1, 1);


        public static GUIStyle Get(Color color, int border)
        {
            if (style == null) style = new GUIStyle();
            if (texture == null) texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            style.normal.background = texture;
            style.margin = new RectOffset(border, border, border, border);
            return style;
        }
    }
}
#endif