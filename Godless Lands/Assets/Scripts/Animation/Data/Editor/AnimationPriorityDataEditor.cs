using Protocol.Data.Replicated.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Animation.Data.Editor
{
    [CustomEditor(typeof(AnimationPriorityData))]
    internal class AnimationPriorityDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
           // base.OnInspectorGUI();

            var animationPriorityData = ((AnimationPriorityData)target);

            foreach (var animationPriority in animationPriorityData.AnimationPriorities)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(animationPriority.AnimationID.ToString());
                animationPriority.SetPriority(EditorGUILayout.IntField(animationPriority.Priority));
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Update"))
            {
                List<AnimationPriority> animationPriorities = animationPriorityData.AnimationPriorities.GroupBy(x => x.AnimationID)
                                                                                                       .Select(g => g.First())
                                                                                                       .ToList();

                foreach(var animtion in Enum.GetValues(typeof(AnimationID)).Cast<AnimationID>())
                {
                    if (animationPriorities.FirstOrDefault(x => x.AnimationID == animtion) == null)
                    {
                        animationPriorities.Add(new AnimationPriority(animtion, 0));
                    }
                }

                animationPriorityData.UpdateData(animationPriorities);
                EditorUtility.SetDirty(animationPriorityData);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
