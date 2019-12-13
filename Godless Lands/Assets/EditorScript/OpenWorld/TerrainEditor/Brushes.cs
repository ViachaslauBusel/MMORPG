#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OpenWorldEditor
{
    //Настройки и список кистей
    public class Brushes
    {

        //  public static int brush = 0;
        //  public static int brush_size = 1;
        public static Brush brush = new Brush(0, 1);
        public static int opacity = 1;

        public static readonly Texture2D[] brushes_texture = { Resources.Load("OpenWorldEditor/Brush_1") as Texture2D,
                                           Resources.Load("OpenWorldEditor/Brush_2") as Texture2D,
                                           Resources.Load("OpenWorldEditor/Brush_3") as Texture2D };

        public static GUIStyle Style
        {
            get
            {

                brush_style.fixedHeight = brush_style.fixedWidth = size * (((Screen.width - 12.0f) / size) / CountOnLine());
                return brush_style;
            }
        }

        private static readonly GUIStyle brush_style = CreateBrushStyle();
        private static readonly float size = 30.0f;

        public static int CountOnLine()
        {
            return (int)((Screen.width - 12.0f) / size);
        }
        private static GUIStyle CreateBrushStyle()
        {

            GUIStyle style = new GUIStyle();
            style.active.background = Resources.Load("Editor/but") as Texture2D;
            style.onNormal.background = Resources.Load("Editor/but") as Texture2D;
            return style;
        }

        public static bool IfChanged(Brush _brush)
        {
            if (brush.brush != _brush.brush) return true;
            if (brush.brush_size != _brush.brush_size) return true;
            return false;
        }

        public static Brush GetBrush()
        {


            if (brush.brush_size < 4) brush.brush_float = CreateBrush();
            else brush.brush_float = ColorToFloat(TextureScale.Bilinear(brushes_texture[brush.brush], brush.brush_size, brush.brush_size));

            return brush;
        }

        private static float[,] CreateBrush()
        {
            float[,] _brush = new float[brush.brush_size, brush.brush_size];
            for (int i = 0; i < _brush.GetLength(0); i++)
            {
                for (int j = 0; j < _brush.GetLength(1); j++)
                {
                    _brush[i, j] = 1.0f;
                }
            }
            return _brush;
        }
        private static float[,] ColorToFloat(Color[] colors)
        {
            float[,] _brush = new float[brush.brush_size, brush.brush_size];
            for (int i = 0; i < _brush.GetLength(0); i++)
            {
                for (int j = 0; j < _brush.GetLength(1); j++)
                {
                    _brush[i, j] = colors[i * _brush.GetLength(1) + j].a;
                }
            }
            return _brush;
        }

        public struct Brush
        {
            public int brush;
            public int brush_size;
            public float[,] brush_float;

            public Brush(int brush, int brush_size)
            {
                this.brush = brush;
                this.brush_size = brush_size;
                brush_float = null;
            }
        }
    }
}
#endif