#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

namespace OpenWorldEditor {

    //Обновлене одного тайла с  обьектами карты
    public class MapEditLoader
    {
        public static void UpdateTileObject(MapLoader mapLoader, int xKM, int yKM, int xTR, int yTR)
        {
            for(int x=0; x<mapLoader.TerrainMap.GetLength(0); x++)
            {
                for (int y = 0; y < mapLoader.TerrainMap.GetLength(1); y++)
                {
                    if (mapLoader.TerrainMap[x, y] == null) continue;
                    TerrainInfo terrainInfo = mapLoader.TerrainMap[x,y].GetComponent<TerrainInfo>();
                    if (terrainInfo == null) continue;
                    if (terrainInfo.xKM == xKM && terrainInfo.yKM == yKM && terrainInfo.xTR == xTR && terrainInfo.yTR == yTR)
                    {
                        UpdateTile(mapLoader.TerrainMap[x, y], mapLoader.map, terrainInfo);
                    }
                }
            }
        }

        private static void UpdateTile(GameObject tile, Map map, TerrainInfo terrainInfo)
        {
            GameObject[] gameObjects = new GameObject[tile.transform.childCount];
            for(int i=0; i<gameObjects.Length; i++)
            {
                gameObjects[i] = tile.transform.GetChild(i).gameObject;
            }
            foreach(GameObject _obj in gameObjects)
            {
                if (_obj.GetComponent<WaterTile>() == null && _obj.GetComponent<Terrain>() == null)
                {
                    GameObject.DestroyImmediate(_obj);
                }
            }

            terrainInfo.mapObjects.Clear();

            //Map Objet
            string path = map.mapName + "/KMObject_" + terrainInfo.xKM + '_' + terrainInfo.yKM + "/TRObject_" + terrainInfo.xTR + '_' + terrainInfo.yTR;


            ObjectElement objectElement = Resources.Load<ObjectElement>(path);


            if (objectElement != null)
            {
                for (int i = 0; i < objectElement.mapObjects.Count; i++)
                {
                    MapObject mapObject = objectElement.mapObjects[i];
                    if (mapObject == null) continue;
                    GameObject _gameObject = GameObject.Instantiate(mapObject.prefab);
                    _gameObject.transform.SetParent(tile.transform);
                    _gameObject.transform.position = mapObject.postion;
                    _gameObject.transform.rotation = mapObject.orientation;
                    _gameObject.transform.localScale = mapObject.scale;

                    terrainInfo.ObjectElement = objectElement;
                    terrainInfo.mapObjects.Add(mapObject.GetHashCode(), _gameObject);
                }
            }
        }
    }

    
}
#endif