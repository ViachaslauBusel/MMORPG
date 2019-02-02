#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace Redactor
{
    public class RedactorStyle
    {
        private GUIStyle backgraund_item_style;
        private GUIStyle backgraund_select_item_style;
        private GUIStyle plus_but_style;
        private GUIStyle hide_but;
        private GUIStyle menu_style;
        private GUIStyle save_on_disk;
        private GUIStyle del_but_style;
        private GUIStyle text_page_style;
        private GUIStyle left_but_style;
        private GUIStyle right_but_style;

        public GUIStyle BackgraundItem { get { return backgraund_item_style; } }
        public GUIStyle BackgraundSelectItem { get { return backgraund_select_item_style; } }
        public GUIStyle Plus { get { return plus_but_style; } }
        public GUIStyle Hide { get { return hide_but; } }
        public GUIStyle Menu { get { return menu_style; } }
        public GUIStyle SaveOnDisk { get { return save_on_disk; } }
        public GUIStyle Delet { get { return del_but_style; } }
        public GUIStyle Text { get { return text_page_style; } }
        public GUIStyle Left { get { return left_but_style; } }
        public GUIStyle Right { get { return right_but_style; } }

        public RedactorStyle()
        {
            Load();
        }

        public void Load()
        {
            //Create style
            backgraund_item_style = CreateButtonStyle("Inventory_Box");
            backgraund_select_item_style = CreateButtonStyle("Inventory_Box_select");
            plus_but_style = CreateButtonStyle("add", "addPres");
            hide_but = CreateButtonStyle(null, "but");


            //create button for Menu
            menu_style = CreateButtonStyle("menu");
            save_on_disk = CreateButtonStyle("save_on_disk", "save_on_disk_2");
            del_but_style = CreateButtonStyle("delet", "delet2");
            left_but_style = CreateButtonStyle("left_normal", "left_active");
            right_but_style = CreateButtonStyle("right_normal", "right_active");

            text_page_style = new GUIStyle();
            text_page_style.alignment = TextAnchor.MiddleCenter;
        }

        private GUIStyle CreateButtonStyle(string _normal, string _active = null)
        {
            GUIStyle _style = new GUIStyle();
            if (_normal != null) _style.normal.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/" + _normal + ".png");// Resources.Load("Editor/"+_normal) as Texture2D;
            if (_active != null) _style.active.background = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Img/" + _active + ".png");// Resources.Load("Editor/"+_active) as Texture2D;
            return _style;
        }


    }
}
#endif