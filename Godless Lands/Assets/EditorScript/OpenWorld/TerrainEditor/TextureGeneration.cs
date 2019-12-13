#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    public class TextureGeneration
    {
        public static void Generation(float height, TerrainLayer terrainLayer, Map editMap)
        {
            Map map = editMap;

            float maxProgress = map.mapSize * map.mapSize;
            float totalProgress = 0.0f;

           

            for (int yKM = 0; yKM < map.mapSize; yKM++)
            {
                for (int xKM = 0; xKM < map.mapSize; xKM++)
                {
                    totalProgress = (yKM * map.mapSize) + xKM;
                    EditorUtility.DisplayProgressBar("OpenWorld", "Texture Generation", totalProgress / maxProgress);
                    for (int y = 0; y < map.blocksCount; y++)
                    {
                        for (int x = 0; x < map.blocksCount; x++)
                        {
                            string path = map.mapName + "/KMBlock_" + xKM + '_' + yKM + "/TRBlock_" + x + '_' + y;

                            MapElement mapElement = Resources.Load<MapElement>(path);

                            if (mapElement.terrainData.terrainLayers == null || mapElement.terrainData.terrainLayers.Length == 0)
                            {
                                if (!ConstainsHeight(mapElement.terrainData, height)) continue;
                                TerrainLayer[] terrainLayers = new TerrainLayer[1];
                                for (int k = 0; k < 1; k++)
                                {
                                    terrainLayers[k] = terrainLayer;
                                }

                                mapElement.terrainData.terrainLayers = terrainLayers;
                                mapElement.terrainData.SetAlphamaps(0, 0, GetMapTexture(mapElement.terrainData.alphamapHeight, mapElement.terrainData.alphamapWidth, terrainLayers.Length));
                            }
                        }
                    }


                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static bool ConstainsHeight(TerrainData terrainData, float height)
        {
            height /= terrainData.size.y;
            float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
            for (int y = 0; y < heights.GetLength(1); y++)
            {
                for (int x = 0; x < heights.GetLength(0); x++)
                {
                    if (heights[x, y] < height) return true;
                }
            }
            return false;
        }

        private static float[,,] GetMapTexture(int x, int y, int z)
        {

            float[,,] map = new float[x, y, z];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    map[i, j, 0] = 1.0f;
                }
            }
            return map;
        }
    }
}
#endif