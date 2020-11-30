#if UNITY_EDITOR
using OpenWorld;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace OpenWorldEditor
{

    public class TerrainExport
    {
        public static void Export(Map map)
        {
           
            float totalProgress = 0; 

          
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/terrain.dat", FileMode.Create)))
            {
                int maxTile = map.mapSize * map.blocksCount;
                float maxProgress = maxTile * maxTile;
                stream_out.Write(map.mapSize);//Max tile
                stream_out.Write(map.blocksCount);//Блоков на километр

                for (int y = 0; y < maxTile; y++)
                {
                  
                    for (int x = 0; x < maxTile; x++)
                    {
                        totalProgress = (y * maxTile) + x;
                        EditorUtility.DisplayProgressBar("OpenWorld", "Export terrain.dat", totalProgress / maxProgress);


                        int xKM = x / 10;
                        int yKM = y / 10;
                        int xTR = x % 10;
                        int yTR = y % 10;
                       
                        Write(xKM,yKM, xTR, yTR, map, stream_out); 
                    }
                }
            }
            EditorUtility.ClearProgressBar();
        }

        private static void Write(int xKM, int yKM, int xTR, int yTR, Map map, BinaryWriter stream_out)
        {
            string folder = map.mapName + "/KMBlock_" + xKM + '_' + yKM;

                    string path = folder + "/TRBlock_" + xTR + '_' + yTR;
                    MapElement mapElement = Resources.Load<MapElement>(path);
           
            if (mapElement != null)
            {
              //  Debug.Log(mapElement.terrainData.heightmapResolution);
                int size = (mapElement.terrainData.heightmapResolution * mapElement.terrainData.heightmapResolution) * sizeof(float);
                stream_out.Write(size);//Размер массива высот
                float[,] heights = mapElement.terrainData.GetHeights(0, 0, mapElement.terrainData.heightmapResolution, mapElement.terrainData.heightmapResolution);
                for (int i = 0; i < mapElement.terrainData.heightmapResolution; i++)
                {
                    for (int j = 0; j < mapElement.terrainData.heightmapResolution; j++)
                    {
                        stream_out.Write(heights[i, j] * mapElement.terrainData.size.y);
                    }
                }
                Resources.UnloadAsset(mapElement);
            }
           
        }
    }
}
#endif