#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ObjectRegistryEditor
{
    /// <summary>
    /// Стили используемые в редакторе
    /// </summary>
    public static class RedactorStyle
    {


        public static GUIStyle BackgraundItem { get; private set; }
        public static GUIStyle BackgraundSelectItem { get; private set; }
        public static GUIStyle Plus { get; private set; }
        public static GUIStyle Hide { get; private set; }
        public static GUIStyle Menu { get; private set; }
        public static GUIStyle SaveOnDisk { get; private set; }
        public static GUIStyle Export { get; private set; }
        public static GUIStyle Delet { get; private set; }
        public static GUIStyle Text { get; private set; }
        public static GUIStyle Left { get; private set; }
        public static GUIStyle Right { get; private set; }

        public static GUIStyle PlayerDialogueTitle { get; private set; }
        public static GUIStyle PlayerDialogueTitleSelect { get; private set; }
        public static GUIStyle NPCDialogueTitle { get; private set; }
        public static GUIStyle NPCDialogueTitleSelect { get; private set; }
        public static GUIStyle ActionTitle { get; private set; }
        public static GUIStyle ActionTitleSelect { get; private set; }
        public static GUIStyle StageBody { get; private set; }
        public  static GUIStyle InPoint { get; private set; }
        public static GUIStyle OutPoint { get; private set; }



        static RedactorStyle()
        {
            //Create style
            BackgraundItem = CreateButtonStyle("Inventory_Box");
            BackgraundSelectItem = CreateButtonStyle("Inventory_Box_select");
            Plus = CreateButtonStyle("add", "addPres");
            Hide = CreateButtonStyle(null, "but");


            //create button for Menu
            Menu = CreateButtonStyle("menu");
            SaveOnDisk = CreateButtonStyle("save_on_disk", "save_on_disk_2");
            Export = CreateButtonStyle("save_on_disk", "save_on_disk_2");
            Delet = CreateButtonStyle("delet", "delet2");
            Left = CreateButtonStyle("left_normal", "left_active");
            Right = CreateButtonStyle("right_normal", "right_active");

            Text = new GUIStyle();
            Text.alignment = TextAnchor.MiddleCenter;

            PlayerDialogueTitle = new GUIStyle();
            PlayerDialogueTitle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node2.png") as Texture2D;
            PlayerDialogueTitle.border = new RectOffset(12, 12, 12, 12);

            PlayerDialogueTitleSelect = new GUIStyle();
            PlayerDialogueTitleSelect.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node3.png") as Texture2D;
            PlayerDialogueTitleSelect.border = new RectOffset(12, 12, 12, 12);

            NPCDialogueTitle = new GUIStyle();
            NPCDialogueTitle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node4.png") as Texture2D;
            NPCDialogueTitle.border = new RectOffset(12, 12, 12, 12);

            NPCDialogueTitleSelect = new GUIStyle();
            NPCDialogueTitleSelect.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node5.png") as Texture2D;
            NPCDialogueTitleSelect.border = new RectOffset(12, 12, 12, 12);

            ActionTitle = new GUIStyle();
            ActionTitle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node6.png") as Texture2D;
            ActionTitle.border = new RectOffset(12, 12, 12, 12);

            ActionTitleSelect = new GUIStyle();
            ActionTitleSelect.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node5.png") as Texture2D;
            ActionTitleSelect.border = new RectOffset(12, 12, 12, 12);

            StageBody = new GUIStyle();
            StageBody.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node0.png") as Texture2D;
            StageBody.border = new RectOffset(20, 20, 30, 30);



            InPoint= new GUIStyle();
            InPoint.normal.background = EditorGUIUtility.Load("d_winbtn_mac_min@2x") as Texture2D;
            InPoint.active.background = EditorGUIUtility.Load("d_winbtn_mac_min_a@2x") as Texture2D;
            InPoint.border = new RectOffset(0, 0, 0, 0);

            OutPoint = new GUIStyle();
            OutPoint.normal.background = EditorGUIUtility.Load("d_winbtn_mac_max@2x") as Texture2D;
            OutPoint.active.background = EditorGUIUtility.Load("d_winbtn_mac_max_a@2x") as Texture2D;
            OutPoint.border = new RectOffset(0, 0, 0, 0);
        }


        private static GUIStyle CreateButtonStyle(string _normal, string _active = null)
        {
            GUIStyle _style = new GUIStyle();
            if (_normal != null) _style.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/" + _normal + ".png");// Resources.Load("Editor/"+_normal) as Texture2D;
            if (_active != null) _style.active.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/" + _active + ".png");// Resources.Load("Editor/"+_active) as Texture2D;
            return _style;
        }

 /*       private GUIStyle CreateButtonStyleUnity(string _normal, string _active = null)
        {
            GUIStyle _style = new GUIStyle();
            if (_normal != null) _style.normal.background = EditorGUIUtility.IconContent(_normal).image as Texture2D;
            if (_active != null) _style.active.background = EditorGUIUtility.IconContent(_active).image as Texture2D;
            return _style;
        }*/
    }
}
#endif