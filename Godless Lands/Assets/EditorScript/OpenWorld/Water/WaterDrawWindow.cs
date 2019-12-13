#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WaterDrawWindow
    {
        private static float height;
        public static void Draw(Map editMap)
        {
            height = EditorGUILayout.FloatField("Water level: ", height);

            if (GUILayout.Button("Generation Water"))
            {
                WaterGeneration.Generation(height, editMap);
            }
        }
    }
}
#endif