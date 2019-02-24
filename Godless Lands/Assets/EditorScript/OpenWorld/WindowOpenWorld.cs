#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorld
{

    
    public class WindowOpenWorld : EditorWindow
    {


       
        private static MapLoader mapLoader;
        private static int activeTools = -1;
        public static WindowOpenWorld window;

        [MenuItem("Window/TerrainEditor")]
        public static void ShowWindow()
        {
            WindowOpenWorld window = EditorWindow.GetWindow<WindowOpenWorld>(false, "TerrainEditor");
            window.minSize = new Vector2(200.0f, 100.0f);
           
        }

        private void OnEnable()
        {
            window = this;
            // Add (or re-add) the delegate.
            SceneView.onSceneGUIDelegate += this.OnSceneGUI;
            WindowSetting.Load();
            WindowActiveTools.Load();
            /*    GameObject findObj = GameObject.Find("MapEditor");
                if (findObj != null) {
                    mapLoader = CreateMap(SceneView.lastActiveSceneView.camera.transform);
                }
                else
                {
                    Debug.Log("obj == null");
                }*/
            //Debug.Log("Terrain editor OnEnable");



        }
        private void OnDisable()
        {
         //   Debug.Log("Terrain editor OnDisable");
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            DestroySceneGUI();
           // if (mapLoader != null) DestroyImmediate(mapLoader.gameObject);
    
        }

        private void OnGUI()
        {
           // GUILayout.Space(20.0f);
       
            

            if(activeTools != WindowActiveTools.Active())//Если поменялся инструмент, удалить используемые ресурсы на сцене
            {
                DestroySceneGUI();
                activeTools = WindowActiveTools.active; 
            }

            switch (activeTools)
            {
                case 0://Редактирование отключено
                    if (mapLoader != null) DestroyImmediate(mapLoader.gameObject);
                    GameObject obj = GameObject.Find("MapEditor");
                    if (obj != null) DestroyImmediate(obj);
                    Resources.UnloadUnusedAssets();
                    return;
                case 1:// Отрисовать инструменты для Редактирование террейна/     
                    if (mapLoader == null)
                    {
                        mapLoader = CreateMap(SceneView.lastActiveSceneView.camera.transform);
                        if (mapLoader == null) return;
                    }
                    WindowTerrain.Draw(WindowSetting.Map, mapLoader);
                    break;
                case 2://Инструменты для Редактирование монстров                 
                    if (mapLoader == null)
                    {
                        mapLoader = CreateMap(SceneView.lastActiveSceneView.camera.transform);
                        if (mapLoader == null) return;
                    }
                    WindowMonsterActiveTools.Draw();
                    break;
                case 3://Отрисовать инструменты для редактирование обьектов
                    if (mapLoader == null)
                    {
                        mapLoader = CreateMap(SceneView.lastActiveSceneView.camera.transform);
                        if (mapLoader == null) return;
                    }
                    WindowObject.Draw(WindowSetting.Map, mapLoader);
                    break;
                case 4://Экпорт на сервер
                    if (WindowSetting.Map == null)
                    {
                        EditorGUILayout.HelpBox("Карта не выбрана", MessageType.Error);
                        return;
                    }
                        WindowExport.Draw(WindowSetting.Map);
                    break;
                case 5://Настройки редактора карты
                    WindowSetting.Draw();
                    break;
                case 6://Инструменты для Редактирование ресурсов   
                    if (mapLoader == null)
                    {
                        mapLoader = CreateMap(SceneView.lastActiveSceneView.camera.transform);
                        if (mapLoader == null) return;
                    }
                    WindowResourcesAcriveTools.Draw();
                    break;
                case 7://Инструменты для Редактирование станков   
                    if (mapLoader == null)
                    {
                        mapLoader = CreateMap(SceneView.lastActiveSceneView.camera.transform);
                        if (mapLoader == null) return;
                    }
                    WindowMachineActiveTools.Draw();
                    break;
            }

            
        }

        public static MapLoader CreateMap(Transform trackingTransform)
        {
           
            if (EditorApplication.isPlaying && trackingTransform.GetComponent<Camera>() != null)
            {
                GameObject _obj = GameObject.Find("MapEditor");
                if (_obj != null) return _obj.GetComponent<MapLoader>();
                return null;
            }
            if (WindowSetting.Map == null)
            {
                EditorGUILayout.HelpBox("Карта не выбрана", MessageType.Error);
                return null;
            }
            GameObject obj = GameObject.Find("MapEditor");
            if (obj != null) DestroyImmediate(obj);
            obj = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Map.prefab"));
            obj.name = "MapEditor";
            MapLoader mapLoader = obj.GetComponent<MapLoader>();
            mapLoader.trackingObj = trackingTransform;
            mapLoader.map = WindowSetting.Map;
            mapLoader.areaVisible = WindowSetting.areaVisible;
            mapLoader.LoadMap();
            RenderSettings.fog = false;
            return mapLoader;

        }
        public static void ReloadMap()
        {
            if(mapLoader != null)
            {
                Transform tracking = mapLoader.trackingObj;
                DestroyImmediate(mapLoader);
                mapLoader = CreateMap(tracking);
            }
        }
        private void OnSceneGUI(SceneView sceneView)
        {
           // OnGUI();
            if (mapLoader != null)
            {

                mapLoader.ChangeBlock();
            }
            else return;

            switch (activeTools)
            {
                case 0://Редактирование отключено
                    break;
                case 1:// Отрисовать инструменты для Редактирование террейна/  
                    TerrainSceneGUI.SceneGUI(sceneView.camera);
                    break;
                case 2://Инструменты для Редактирование монстров
                    if (WindowMonsterActiveTools.MontersDraw)
                    { MonsterSceneGUI.SceneGUI(sceneView.camera); }
        
                        MonsterVisibleSceneGUI.SceneGUI(mapLoader);
                    break;
                case 3://Отрисовать инструменты для редактирование обьектов
                    WindowOpenWorld.window.Repaint();
                    break;
                case 4://Экпорт на сервер

                    break;
                case 6:
                    if (WindowResourcesAcriveTools.ResourcesDraw)
                    { ResourcesSceneGUI.SceneGUI(sceneView.camera); }

                    ResourcesVisibleSceneGUI.SceneGUI(mapLoader);
                    break;
                case 7:
               /*     if (WindowMachineActiveTools.ResourcesDraw)
                    { ResourcesSceneGUI.SceneGUI(sceneView.camera); }*/

                    MachineVisibleSceneGUI.SceneGUI(mapLoader);
                    break;
            }
            
         
        }

        private void DestroySceneGUI()
        {
            TerrainSceneGUI.Destroy();
            MonsterSceneGUI.Destroy();
            ResourcesSceneGUI.Destroy(); 
            MonsterVisibleSceneGUI.Destroy();
            ResourcesVisibleSceneGUI.Destroy();
            MachineVisibleSceneGUI.Destroy();
        }

        private void OnDestroy()
        {

            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            DestroySceneGUI();
            if (mapLoader != null) DestroyImmediate(mapLoader.gameObject);
        }

 
    }
    
}
#endif