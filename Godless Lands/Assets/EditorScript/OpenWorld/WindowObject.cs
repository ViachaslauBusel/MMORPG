#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OpenWorld
{
    public class WindowObject
    {
        private static int tools = 0;
        private static Map editMap;
        private static MapLoader mapLoader;
        private static Vector2 scrollPos = Vector2.zero;


        public static void Draw(Map _editMap, MapLoader loader)
        {
            editMap = _editMap;
            mapLoader = loader;
            GUILayout.Space(15.0f);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            //Добавление обьекта на карту
            GUI.enabled = tools != 1;
            if (GUILayout.Button(EditorGUIUtility.IconContent("Animation.AddEvent"), EditorStyles.miniButtonLeft, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {
  
                tools = 1;
            }

            //Добавление обьекта в список префабов
            GUI.enabled = tools != 2;
            if (GUILayout.Button(EditorGUIUtility.IconContent("UnityLogo"), EditorStyles.miniButtonRight, GUILayout.Width(25.0f), GUILayout.Height(25.0f)))
            {

                tools = 2;
            }


            GUI.enabled = true;
           GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            DrawActiveTool();
        }



        public static void DrawActiveTool()
        {
            switch (tools)
            {
                case 1:
                    AtachObject();
                    break;
                case 2:
                    ShowObject();
                    break;
            }
        }

        private static void ShowObject()
        {
            if (Selection.activeObject == null) return;

            GameObject _obj = Selection.activeObject as GameObject;
            if (_obj == null) return;
            TerrainInfo terrainInfo = _obj.GetComponentInParent<TerrainInfo>();

            if (terrainInfo == null) return;

            ObjectElement objectElement = terrainInfo.ObjectElement;

            if (objectElement == null || objectElement.mapObjects == null) return;

            List<MapObject> mapObjects = new List<MapObject>();
            mapObjects.AddRange(objectElement.mapObjects);

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            foreach(MapObject mapObj in mapObjects)
            {
                if (mapObj == null) continue;
                GUILayout.Space(10.0f);
                GUILayout.BeginHorizontal();

                GUILayout.Label(mapObj.prefab.name);

                if (!terrainInfo.mapObjects.ContainsKey(mapObj.GetHashCode())) continue;
                GUI.enabled = Selection.activeObject != terrainInfo.mapObjects[mapObj.GetHashCode()];
                if (GUILayout.Button("Select"))
                {
                    Selection.activeObject = terrainInfo.mapObjects[mapObj.GetHashCode()];
                }
                GUI.enabled = true;

                if(GUILayout.Button("Detach")){
                    objectElement.mapObjects.Remove(mapObj);
                    GameObject.DestroyImmediate(terrainInfo.mapObjects[mapObj.GetHashCode()]);
    
                    GameObject objPrefab = PrefabUtility.InstantiatePrefab(mapObj.prefab) as GameObject;
                    objPrefab.transform.position = mapObj.postion;
                    objPrefab.transform.rotation = mapObj.orientation;
                    objPrefab.transform.localScale = mapObj.scale;
                    Save(objectElement);
                }
                
                if (GUILayout.Button("Delet"))
                {
                    objectElement.mapObjects.Remove(mapObj);
                    GameObject.DestroyImmediate(terrainInfo.mapObjects[mapObj.GetHashCode()]);
                    Save(objectElement);
                }

                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }

        private static void AtachObject()
        {
            GUILayout.Space(15.0f);
            if (GUILayout.Button("Attach select Object"))
            {
                AttachObjectOnMap(Selection.gameObjects);
            }
            GUILayout.Space(10.0f);
            if (GUILayout.Button("Attach all Objects"))
            {
               

                List<GameObject> rootObjects = new List<GameObject>();
                Scene scene = SceneManager.GetActiveScene();
                scene.GetRootGameObjects(rootObjects);

                AttachObjectOnMap(rootObjects.ToArray());
            }
        }

        private static void AttachObjectOnMap(GameObject[] gameObjects)
        {
            List<AttachObject> attachObjects = new List<AttachObject>();
            foreach (GameObject obj in gameObjects)
            {
                if (obj == null || obj.transform.parent != null) continue;//Если обьект является Child
                GameObject objPrefab = PrefabUtility.GetCorrespondingObjectFromSource<GameObject>(obj);
                if (objPrefab == null) continue;//Если у обьекта нет префаба
                string pathToPrefab = AssetDatabase.GetAssetPath(objPrefab);
                if (string.IsNullOrEmpty(pathToPrefab)) continue;//Если у обьекта нет префаба
                if (!pathToPrefab.Contains("/Resources/")) continue;//Если префаб находиться не в папке Resources

                attachObjects.Add(new AttachObject(obj, objPrefab));//Добавить обьект в массив на добовление на карту  
            }

            if (attachObjects.Count == 0) return;//Если добовляемых обьектов нет

            string names = "";
            for (int i = 0; i < attachObjects.Count; i++){ if (i != 0) names += ", "; names += attachObjects[i].sceneObj.name; }
             

            if (EditorUtility.DisplayDialog("Attach Object", "Добавить " +names + " на карту?", "Да", "Нет"))
            {
                foreach(AttachObject attachObject in attachObjects)
                {
                    int xKMBlock = (int)(attachObject.sceneObj.transform.position.x / 1000.0f);
                    int yKMBlock = (int)(attachObject.sceneObj.transform.position.z / 1000.0f);
                    int xTRBlock = (int)((attachObject.sceneObj.transform.position.x % 1000.0f) / editMap.blockSize);
                    int yTRBlock = (int)((attachObject.sceneObj.transform.position.z % 1000.0f) / editMap.blockSize);
                    string path = "Assets/Resources/" + editMap.mapName + "/KMObject_" + xKMBlock + '_' + yKMBlock + "/";
                    string fileName = "TRObject_" + xTRBlock + '_' + yTRBlock + ".asset";

                    ObjectElement objectElement = AssetDatabase.LoadAssetAtPath<ObjectElement>(path + fileName);

                    if (objectElement == null)
                    {

                        objectElement = ScriptableObject.CreateInstance<ObjectElement>();
                        Directory.CreateDirectory(path);
                        AssetDatabase.CreateAsset(objectElement, path + fileName);
                    }
                    MapObject mapObject = new MapObject();
                    mapObject.Set(attachObject.prefabObj, attachObject.sceneObj);
                    objectElement.Add(mapObject);
                    Save(objectElement);

                    MapEditLoader.UpdateTileObject(mapLoader, xKMBlock, yKMBlock, xTRBlock, yTRBlock);
                    GameObject.DestroyImmediate(attachObject.sceneObj);
                }
            }
        }

        private static void Save(ObjectElement objectElement)
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(objectElement);
            AssetDatabase.SaveAssets();
        }

        private struct AttachObject
        {
            public GameObject sceneObj;
            public GameObject prefabObj;

            public AttachObject(GameObject sceneObj, GameObject prefabObj)
            {
                this.sceneObj = sceneObj;
                this.prefabObj = prefabObj;
            }
        }
    }
}
#endif