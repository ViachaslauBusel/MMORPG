#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class TextureDrawWindow
    {
        private static float height;
        private static TerrainLayer terrainLayer;
        public static void Draw(Map editMap)
        {
            height = EditorGUILayout.FloatField("height : ", height);
            terrainLayer = EditorGUILayout.ObjectField("Terrain Layer:", terrainLayer, typeof(TerrainLayer), false) as TerrainLayer;
            if (GUILayout.Button("Set Texture"))
            {
                TextureGeneration.Generation(height, terrainLayer, editMap);
            }
        }
    }
}
#endif