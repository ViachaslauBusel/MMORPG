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
        private static GUIStyle stageTitSelectleStyle;
        private static GUIStyle answerStyle;
        private static GUIStyle inPointStyle;
        private static GUIStyle butDeletStyle;
        private static GUIStyle outPointStyle;
        private static GUIStyle selectedStageStyle;


        public static GUIStyle Answer
        {
            get
            {
                if (answerStyle == null)
                {
                    answerStyle = new GUIStyle();
                    answerStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
                    answerStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return answerStyle;
            }
        }
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
        public static GUIStyle StageTitleSelect
        {
            get
            {
                if (stageTitSelectleStyle == null)
                {
                    stageTitSelectleStyle = new GUIStyle();
                    stageTitSelectleStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
                    stageTitSelectleStyle.border = new RectOffset(12, 12, 12, 12);
                }
                return stageTitSelectleStyle;
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
        public static GUIStyle ButtonDelet
        {
            get
            {
                if (butDeletStyle == null)
                {
                    butDeletStyle = new GUIStyle();
                    butDeletStyle.normal.background = EditorGUIUtility.Load("d_P4_DeletedRemote") as Texture2D;
                    butDeletStyle.active.background = EditorGUIUtility.Load("d_P4_DeletedLocal") as Texture2D;
                    butDeletStyle.border = new RectOffset(0, 0, 0, 0);
                }
                return butDeletStyle;
            }
        }
        public static GUIStyle OutPoint
        {
            get
            {
                if (outPointStyle == null)
                {
                    outPointStyle = new GUIStyle();
                    outPointStyle.normal.background = EditorGUIUtility.Load("d_winbtn_mac_max") as Texture2D;
                    outPointStyle.active.background = EditorGUIUtility.Load("d_winbtn_mac_max_a") as Texture2D;
                    outPointStyle.border = new RectOffset(0, 0, 0, 0);
                }
                return outPointStyle;
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