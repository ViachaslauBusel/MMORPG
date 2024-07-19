using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Test
{
    [CustomEditor(typeof(FromOldToNewMap))]
    internal class FromOldToNewMapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Convert"))
            {
                var fromOldToNewMap = (FromOldToNewMap)target;
                fromOldToNewMap.Convert();
            }
        }
    }
}
