#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenWorld
{
    public class TerrainOrientation
    {

        public static TerrainInfo Get(TerrainInfo terrainInfo, Orientation orientation)
        {
            GameObject[,] terrainMap = terrainInfo.transform.parent.GetComponent<MapLoader>().TerrainMap;
            Vector2Int point = GetPoint(terrainMap, terrainInfo);
            if (point.x < 0) return null;

            switch (orientation)
            {
                case Orientation.Right:
                    point.x++;
                    break;
                case Orientation.Left:
                    point.x--;
                    break;
                case Orientation.Up:
                    point.y--;
                    break;
                case Orientation.Down:
                    point.y++;
                    break;
            }

            if (point.x < 0 || point.x >= terrainMap.GetLength(0)) return null;
            if (point.y < 0 || point.y >= terrainMap.GetLength(1)) return null;
            if(terrainMap[point.x, point.y] == null) return null;
            return terrainMap[point.x, point.y].GetComponent<TerrainInfo>();
        }

        public static Vector2Int GetPoint(GameObject[,] terrainMap, TerrainInfo terrainInfo)
        {
            if(terrainInfo == null) { Debug.Log("terrainInfo == null"); return new Vector2Int(-1, -1); }
            for (int x = 0; x < terrainMap.GetLength(0); x++)
            {
                for (int y = 0; y < terrainMap.GetLength(1); y++)
                {
                    if (terrainMap[x, y] == null) continue;
                    if (terrainMap[x, y].GetComponent<TerrainInfo>().Equals(terrainInfo)) return new Vector2Int(x, y);
                }
            }
            return new Vector2Int(-1, -1);
        }
    }
    
}
#endif