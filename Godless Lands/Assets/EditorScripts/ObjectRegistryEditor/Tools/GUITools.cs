#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRegistryEditor.Tools
{
    public static class GUITools
    { 



        public static void DrawInCenter(Action action)
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            action?.Invoke();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
        public static void DrawInMiddle(Action action)
        {
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            action?.Invoke();
            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }
    }
}
#endif