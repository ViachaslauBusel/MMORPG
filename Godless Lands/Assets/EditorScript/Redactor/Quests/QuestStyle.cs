#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestsRedactor
{
    public class QuestStyle
    {
        private static GUIStyle stageBodyStyle;
        private static GUIStyle stageTitleStyle;
        private static GUIStyle inPointStyle;
        private static GUIStyle outPointStyle;
        private static GUIStyle selectedStageStyle;


        public static GUIStyle StageBody
        {
            get
            {
                if (stageBodyStyle == null)
                {
                    stageBodyStyle = new GUIStyle();
                    stageBodyStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
                    stageBodyStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return stageBodyStyle;
            }
        }
        public static GUIStyle StageTitle
        {
            get
            {
                if (stageTitleStyle == null)
                {
                    stageTitleStyle = new GUIStyle();
                    stageTitleStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2.png") as Texture2D;
                    stageTitleStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return stageTitleStyle;
            }
        }
        public static GUIStyle InPoint
        {
            get
            {
                if (inPointStyle == null)
                {
                    inPointStyle = new GUIStyle();
                    inPointStyle.normal.background = EditorGUIUtility.Load("d_winbtn_mac_max") as Texture2D;
                    inPointStyle.active.background = EditorGUIUtility.Load("d_winbtn_mac_max_a") as Texture2D;
                    inPointStyle.border = new RectOffset(0, 0, 0, 0);
                }
                return inPointStyle;
            }
        }
        public static GUIStyle SelectedStage
        {
            get
            {
                if (selectedStageStyle == null)
                {
                    selectedStageStyle = new GUIStyle();
                    selectedStageStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
                    selectedStageStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return inPointStyle;
            }
        }

    }
}
#endif