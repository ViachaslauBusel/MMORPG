using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Network.Object.Visualization.Entities.Characters.Editor
{
    [CustomEditor(typeof(BodyPartData))]
    internal class BodyPartDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Bake"))
            {
                var bodyPartData = (BodyPartData)target;
                bodyPartData.Bake();
            }
        }
    }
}
