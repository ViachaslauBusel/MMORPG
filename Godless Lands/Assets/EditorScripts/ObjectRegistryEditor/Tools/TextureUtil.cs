using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObjectRegistryEditor.Tools
{
    public class TextureUtil
    {
        public static void SetTransparent(Texture2D texture)
        {
            if (texture == null) return;
            Color32[] colors = texture.GetPixels32();
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].r == colors[0].r && colors[i].g == colors[0].g && colors[i].b == colors[0].b) colors[i].a = 0;
            }
            texture.SetPixels32(colors);
            texture.Apply();
        }
    }
}