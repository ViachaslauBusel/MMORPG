#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{
    //Создание карты
    public class MapGeneration
    {
        private static float maxProgress;
        private static float totalProgress;

        public static bool GenerationWorld(string worldName, Map map)
        {
            string path = "Assets/Resources/" + worldName;
            Directory.CreateDirectory(path);

            maxProgress = map.mapSize * map.mapSize;

            string folder;
            for (int x=0; x< map.mapSize; x++)
            {
                EditorUtility.DisplayProgressBar("OpenWorld", "Map Generation",totalProgress / maxProgress);
                for (int y=0; y< map.mapSize; y++)
                {

                    totalProgress = (x * map.mapSize)+y;

                    folder = path + "/KMBlock_" + x + '_' + y;
                    Directory.CreateDirectory(folder);
                    GenerationTerrain(folder, map);
                }
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return true;
        }

        private static void GenerationTerrain(string folder, Map map)
        {
            float height = map.setHeight / map.height;
            for (int x = 0; x < map.blocksCount; x++)
            {
                EditorUtility.DisplayProgressBar("OpenWorld", "Map Generation", (totalProgress + (x / (float)map.blocksCount)) / maxProgress);
                for (int y = 0; y < map.blocksCount; y++)
                {
                    TerrainData terrainData = new TerrainData();
                    terrainData.name = "/TRData_" + x + '_' + y;
                   
                    terrainData.heightmapResolution = map.heightmapResolution;
                    terrainData.alphamapResolution = 256;

                   terrainData.size = new Vector3(map.blockSize, 600.0f, map.blockSize);

                    MapElement mapElement = ScriptableObject.CreateInstance<MapElement>();
                   
                   // AssetDatabase.AddObjectToAsset(mapElement.terrainData, mapElement);
                    AssetDatabase.CreateAsset(mapElement, folder+ "/TRBlock_" + x + '_' + y + ".asset");

                    AssetDatabase.AddObjectToAsset(terrainData, mapElement);
                  //  AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(terrainData));
                    mapElement.terrainData = terrainData;

                    /*   TerrainLayer[] terrainLayers = new TerrainLayer[1];
                       for (int k = 0; k < 1; k++)
                       {
                        terrainLayers[k] = Resources.Load("Gras1") as TerrainLayer;
                       }

                    terrainData.terrainLayers = terrainLayers;
                       terrainData.SetAlphamaps(0, 0, GetMapTexture(terrainData.alphamapHeight, terrainData.alphamapWidth, terrainLayers.Length));*/

                    float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);

                    for (int yh = 0; yh < heights.GetLength(1); yh++)
                    {
                        for (int xh = 0; xh < heights.GetLength(0); xh++)
                        {
                            heights[xh, yh] = height;
                        }
                    }
                    terrainData.SetHeights(0, 0, heights);
                }
               
            }
        }
       
    }
}
#endif