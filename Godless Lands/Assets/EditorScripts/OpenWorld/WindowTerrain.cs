#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using OpenWorld;

namespace OpenWorldEditor
{

   

    public class WindowTerrain
    {

        private static int tools = 4;
        

        public static void DrawTools()
        {
            GUILayout.Space(20.0f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

           /* GUI.enabled = tools != 0;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolRaise"), EditorStyles.miniButtonLeft)) { tools = 0; }
            GUI.enabled = tools != 1;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolSetHeight"), EditorStyles.miniButtonMid)) { tools = 1; }
            GUI.enabled = tools != 2;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolSmoothHeight"), EditorStyles.miniButtonMid)) { tools = 2; }
            GUI.enabled = tools != 3;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolSplat"), EditorStyles.miniButtonMid)) { tools = 3; }*/
            GUI.enabled = tools != 4;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolTrees"), EditorStyles.miniButtonLeft)) { tools = 4; }
            GUI.enabled = tools != 5;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolPlants"), EditorStyles.miniButtonMid)) { tools = 5; }
            GUI.enabled = tools != 6;
            if (GUILayout.Button(EditorGUIUtility.IconContent("TerrainInspector.TerrainToolSettings"), EditorStyles.miniButtonRight)) { tools = 6; }
            GUI.enabled = true;

       

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }



        public static bool IfActiveHeightmapBrushes()
        {
            return tools == 0 || tools == 1 || tools == 2;
        }
        public static bool IfActiveAlphamapBrushes()
        {
            return tools == 3;
        }
        public static bool IfActiveSetting()
        {
            return tools == 6;
        }
        public static bool IfActiveTree()
        {
            return tools == 4;
        }
        public static bool IfActiveGrass()
        {
            return tools == 5;
        }
        public static bool IfChanged(ref int _tools) 
        {
           if(tools != _tools)
            {
                _tools = tools;
                return true;
            }
            return false;
        }

        public static HeightMapSetting heightMapSetting;
        private static int error = 0;


        public static void Draw(Map editMap, MapLoader mapLoader)
        {
            if (mapLoader == null) return;

            WindowTerrain.DrawTools();
            WindowTerrain.DrawHelp();

            if (WindowTerrain.IfActiveHeightmapBrushes())
            {
                WindowTerrain.DrawBrushes();
                WindowTerrain.DrawBrushSetting();
            }
            else if (WindowTerrain.IfActiveAlphamapBrushes())
            {
                WindowTerrain.DrawBrushes();
                WindowTerrain.DrawBrushSetting();
            }
            else if (WindowTerrain.IfActiveSetting())
            {
                if (WindowTerrain.DrawSetting(editMap))
                {
                    if (mapLoader != null) GameObject.DestroyImmediate(mapLoader.gameObject);
                    HeightMapFromTexture.Apply(editMap, WindowTerrain.heightMapSetting);
                }
            }
            else if (WindowTerrain.IfActiveTree())
            {
                WaterDrawWindow.Draw(editMap);
            }
            else if (WindowTerrain.IfActiveGrass())
            {
                TextureDrawWindow.Draw(editMap);
            }
        }

        public static bool DrawSetting(Map map)
        {
            GUILayout.Space(10.0f);

            if (GUILayout.Button("Open PNG"))
            {
                string path = EditorUtility.OpenFilePanel("Open height map png", "", "png");
                if (!string.IsNullOrEmpty(path))
                {
                    byte[] file = File.ReadAllBytes(path);
                    heightMapSetting.texture = new Texture2D(2, 2);
                    Debug.Log(heightMapSetting.texture.LoadImage(file));
                }
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Height map: ");
            GUILayout.Label(heightMapSetting.texture, GUILayout.Width(100.0f), GUILayout.Height(100.0f));
            GUILayout.EndHorizontal();

            heightMapSetting.minHeight = EditorGUILayout.FloatField("Min Height: ", heightMapSetting.minHeight);
            heightMapSetting.maxHeight = EditorGUILayout.FloatField("Max height: ", heightMapSetting.maxHeight);

            if (GUILayout.Button("Apply"))
            {
                if(heightMapSetting.texture == null) { error = 1; return false; }
                if (heightMapSetting.minHeight < 0.0f || heightMapSetting.minHeight >= heightMapSetting.maxHeight) { error = 2; return false; }
                if (heightMapSetting.maxHeight < heightMapSetting.minHeight || heightMapSetting.maxHeight > map.height) { error = 3; return false; }
                error = 0;
                return true;
            }
            return false;
        }

        public static void DrawError()
        {
            switch (error)
            {
                case 1:
                    EditorGUILayout.HelpBox("Текстура не выбрана", MessageType.Error);
                    break;
                case 2:
                    EditorGUILayout.HelpBox("Неверная минимальная высота", MessageType.Error);
                    break;
                case 3:
                    EditorGUILayout.HelpBox("Неверная максимальная высота", MessageType.Error);
                    break;
            }
        }

        public static void DrawBrushes()
        {
            GUILayout.Space(10.0f);
            GUILayout.Label("Brushes", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal(GUI.skin.box);
            Brushes.brush.brush = GUILayout.SelectionGrid(Brushes.brush.brush, Brushes.brushes_texture, Brushes.CountOnLine(), Brushes.Style);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        public static void DrawBrushSetting()
        {
            GUILayout.Space(5.0f);
            GUILayout.Label("Settings", EditorStyles.boldLabel);


            Brushes.brush.brush_size = EditorGUILayout.IntSlider("  Brush size:", Brushes.brush.brush_size, 1, 100);
            Brushes.opacity = EditorGUILayout.IntSlider("  Opacity:", Brushes.opacity, 1, 100);

        }

        public static void DrawHelp()
        {
            GUILayout.Space(5.0f);
            switch (tools)
            {
                case 0:
                    EditorGUILayout.HelpBox("Raise/Lower Terrain\n\nClick to raise.\n\nHold shift and click to lower", MessageType.None);
                    break;
                case 3:
                    EditorGUILayout.HelpBox("Paint Texture\n\nSelect a texture below, then click to paint.", MessageType.None);
                    break;
                    
            }
        }
    }
}
#endif