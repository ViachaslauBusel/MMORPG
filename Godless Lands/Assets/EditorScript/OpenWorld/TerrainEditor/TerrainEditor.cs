#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorld
{

    public class TerrainEditor
    {

       public static void Raise(TerrainInfo centerTerrain, Vector2Int cellCenter, float[,] mapHeight)
        {
            int xCenter = mapHeight.GetLength(0) / 2;
            int yCenter = mapHeight.GetLength(1) / 2;
            if (mapHeight.GetLength(0) % 2 == 0) xCenter--;
            if (mapHeight.GetLength(1) % 2 == 0) yCenter--;

            int xTerrain;
            int yTerrain;
            TerrainInfo terrain;

            List<TerrainPoints> terrainPoints = new List<TerrainPoints>();

            for (int x=0; x<mapHeight.GetLength(0); x++)
            { 
                for(int y=0; y<mapHeight.GetLength(1); y++)
                {
                     xTerrain = cellCenter.x - (xCenter - x);
                     yTerrain = cellCenter.y - (yCenter - y);
                   
                    terrain = centerTerrain;
                    while (xTerrain < 0)
                    {
                        terrain = TerrainOrientation.Get(terrain, Orientation.Left); // terrain.GetComponent<TerrainInfo>().left;
                        if (terrain == null) return;//TODO
                        xTerrain = (terrain.terrainData.heightmapWidth-1) + xTerrain;
                    }
                    while (yTerrain < 0)
                    {
                        terrain = TerrainOrientation.Get(terrain, Orientation.Up);
                        if (terrain == null) return;//TODO
                        yTerrain = (terrain.terrainData.heightmapHeight - 1) + yTerrain;
                    }
                    while (xTerrain >= terrain.terrainData.heightmapWidth - 1)
                    {
                        xTerrain -= (terrain.terrainData.heightmapWidth - 1);
                        terrain = TerrainOrientation.Get(terrain, Orientation.Right);
                        if (terrain == null) return;//TODO
                    }
                    while (yTerrain >= terrain.terrainData.heightmapHeight - 1)
                    {
                        yTerrain -= (terrain.terrainData.heightmapHeight - 1);
                        terrain = TerrainOrientation.Get(terrain, Orientation.Down);
                        if (terrain == null) return;//TODO
                    }

                    terrainPoints.AddPoint(terrain.GetComponent<TerrainInfo>(), xTerrain, yTerrain, mapHeight[x, y]);
                   // RaisePoint(terrain, xTerrain, yTerrain, mapHeight[x,y]);
                }
            }

            RaisePoint(terrainPoints);
        }

        private static void RaisePoint(List<TerrainPoints> terrainPoints)
        {
            List<TerrainPoints> trEqualizePoints = new List<TerrainPoints>();
            foreach (TerrainPoints trPoint in terrainPoints)
            {
                TerrainInfo terrain = trPoint.GetTerrainInfo();
                float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);

                foreach (Point point in trPoint.GetPoints())
                {
                    float raise = heights[point.y, point.x];
                    raise += 0.04f * point.smooth * (Brushes.opacity / 100.0f);
                    if (raise > 1.0f) raise = 1.0f;

                    if (point.raise)
                    {
                        heights[point.y, point.x] = point.smooth;
                        continue;
                    }
                    if (point.x == 0)
                    {
                        TerrainInfo left = TerrainOrientation.Get(terrain, Orientation.Left);
                        if (left == null) return;
                        trEqualizePoints.AddPoint(left, left.terrainData.heightmapWidth - 1, point.y, raise, true);
                    }
                    if (point.y == 0)
                    {
                        TerrainInfo up = TerrainOrientation.Get(terrain, Orientation.Up);
                        if (up == null) return;
                        trEqualizePoints.AddPoint(up,  point.x, up.terrainData.heightmapHeight - 1, raise, true);
                    }
                    if(point.x == 0 && point.y == 0)
                    {
                        TerrainInfo corner = TerrainOrientation.Get(terrain, Orientation.Up);
                        corner = TerrainOrientation.Get(corner, Orientation.Left);
                        if (corner == null) return;
                        trEqualizePoints.AddPoint(corner, corner.terrainData.heightmapWidth - 1, corner.terrainData.heightmapHeight - 1, raise, true);
                    }

                    heights[point.y, point.x] = raise;
                }
                terrain.terrainData.SetHeights(0, 0, heights);

            }

           if(trEqualizePoints.Count > 0) RaisePoint(trEqualizePoints);
        }



    }
}
#endif