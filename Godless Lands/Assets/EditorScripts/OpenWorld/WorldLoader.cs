#if UNITY_EDITOR
using OpenWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class WorldLoader
    {
        private static MapLoader mapLoader;

        public static MapLoader Map
        {
            get
            {
                if (mapLoader == null)
                {
                    GameObject obj = GameObject.Find("MapEditor");
                    if(obj != null)
                       mapLoader = obj.GetComponent<MapLoader>();
                    
                    if (mapLoader == null)
                        mapLoader = CreateMap();
                }
                return mapLoader;
            }
        }
        /// <summary>
        /// Карта может быть создана?
        /// </summary>
        public static bool IsHaveMap
        {
            get { return Map != null; }
        }
       /// <summary>
       /// Карта ест на сцене?
       /// </summary>
        public static bool IsMap { get { return mapLoader != null; } }
        public static void Destroy()
        {
            if (mapLoader != null) GameObject.DestroyImmediate(mapLoader.gameObject);
            GameObject obj = GameObject.Find("MapEditor");
            if (obj != null) GameObject.DestroyImmediate(obj);
            Resources.UnloadUnusedAssets();
        }

        public static void Reload()
        {
            if (mapLoader != null)
            {
                Transform tracking = mapLoader.trackingObj;
                GameObject.DestroyImmediate(mapLoader.gameObject);
                mapLoader = Map;
            }
        }

        public static MapLoader CreateMap()
        {

            //Если игра запущена
          /*  if (EditorApplication.isPlaying && trackingTransform.GetComponent<Camera>() != null)
            {
                GameObject _obj = GameObject.Find("MapEditor");
                if (_obj != null) return _obj.GetComponent<MapLoader>();
                return null;
            }*/
            if (WindowSetting.Map == null)
            {
                EditorGUILayout.HelpBox("Карта не выбрана", MessageType.Error);
                return null;
            }
            Transform target = FindTarget();
            if (target == null) return null;

            GameObject obj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Map.prefab"));
            obj.name = "MapEditor";
            MapLoader mapLoader = obj.GetComponent<MapLoader>();
            mapLoader.trackingObj = target;
            mapLoader.map = WindowSetting.Map;
            mapLoader.areaVisible = WindowSetting.areaVisible;
            mapLoader.LoadMap();
            RenderSettings.fog = false;
            return mapLoader;

        }

        private static Transform FindTarget()
        {
            Camera _camera = SceneView.lastActiveSceneView.camera;
            if (_camera == null) return null;
            return _camera.transform;
        }
        /// <summary>
        /// Обновление позиции обьекта вокруг которой отрисовается карта
        /// </summary>
        internal static void Update()
        {
            MapLoader mapLoader = Map;
            if(mapLoader.trackingObj == null)//Если карта потеряла обьект слежения
            {
                Transform target = FindTarget();
                if (target == null) return;
                mapLoader.trackingObj = target;
            }
            mapLoader.ChangeBlock();
        }
    }
}
#endif