#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OpenWorld
{

    [ExecuteInEditMode]
    public class ProjectorBrush : MonoBehaviour {

        public Material material;
        public GameObject pref_projector;
        private GameObject projector_brush;
        public Brushes.Brush brush;
        private int tools = -1;
        private float pixelPerSquare = 20.0f;
        private int indent = 10;
        private static ProjectorBrush projectorBrush;

        private void Start()
        {
            if (projectorBrush != null) DestroyImmediate(projectorBrush.gameObject);
            projectorBrush = this;
        }

        public void DrawBrush(Vector3 center, float cellSize)
        {

            if (projector_brush == null || Brushes.IfChanged(brush) || WindowTerrain.IfChanged(ref tools)) {

                DestroyBrush();
                brush = Brushes.GetBrush();
                projector_brush = Instantiate(pref_projector);
                projector_brush.transform.SetParent(transform);

                     Projector projector = projector_brush.GetComponent<Projector>();
                float lenght = brush.brush_float.GetLength(0) * cellSize;
                float pixel_lenght = brush.brush_float.GetLength(0) * pixelPerSquare;
                     projector.orthographicSize = (lenght *  ((pixel_lenght + (indent * 2 + 2)) / pixel_lenght)) / 2.0f;


                   projector.material.SetTexture("_ShadowTex", CreateTexture(brush.brush_float));
            }
 
            projector_brush.transform.position = center;

        }

         private Texture2D CreateTexture(float[,] brush_float)
          {
            int del = indent;
            int size;
            if (brush_float.GetLength(0) % 2 == 0)
            {
                del += (int)pixelPerSquare;
                size = (int)((brush_float.GetLength(0)+1) * pixelPerSquare + indent * 2);
            }
            else size = (int) (brush_float.GetLength(0) * pixelPerSquare + indent * 2);
              Texture2D texture = new Texture2D(size, size);
              for (int x = 0; x < size; x++)
              {
                  for (int y = 0; y < size; y++)
                  {
                    if (x >= del && x < (size - indent) && y >= del && y < (size - indent))
                    {
                        float alpha = brush_float[(x - del) / (int)pixelPerSquare, (y - del) / (int)pixelPerSquare] - 0.2f;
                        if (alpha < 0.0f) alpha = 0.0f;
                        texture.SetPixel(x, y, new Color(0.2549f, 0.411f, 1.0f, alpha));
                    }
                    else
                        texture.SetPixel(x, y, new Color(0.0f, 0.0f, 0.0f, 0.0f));
                  }
              }

              // texture.filterMode = FilterMode.Bilinear;
              texture.wrapMode = TextureWrapMode.Clamp;
              //   texture.Compress(false);
              texture.Apply();
              return texture;
          }


        private void DestroyBrush()
        {
            if (projector_brush == null) return;
            DestroyImmediate(projector_brush);
        }


    }
}
#endif