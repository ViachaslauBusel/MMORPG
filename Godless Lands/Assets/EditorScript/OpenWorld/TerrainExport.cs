#if UNITY_EDITOR
using OpenWorld;
using System;
using System.Collections;
using System.Collections.Generic;
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
                stream_out.Write(maxTile);//Max tile

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
                // stream_out.Write(xKM * map.blocksCount + x);//Позиция по Х
                //   stream_out.Write(yKM * map.blocksCount + y);//Позиция по Y
                int size = (mapElement.terrainData.heightmapHeight * mapElement.terrainData.heightmapWidth) * 4;
                stream_out.Write(size);//Размер массива высот
                float[,] heights = mapElement.terrainData.GetHeights(0, 0, mapElement.terrainData.heightmapWidth, mapElement.terrainData.heightmapHeight);
                for (int i = 0; i < mapElement.terrainData.heightmapHeight; i++)
                {
                    for (int j = 0; j < mapElement.terrainData.heightmapWidth; j++)
                    {
                        //   stream_out.Write(mapElement.terrainData.size.y * heights[i, j]);//Точка высоты 
                        byte[] _data = BitConverter.GetBytes(mapElement.terrainData.size.y * heights[i, j]);
                        byte[] damp = new byte[4];
                        damp[0] = _data[3];
                        damp[1] = _data[2]; 
                        damp[2] = _data[1];
                        damp[3] = _data[0];
                        stream_out.Write(damp, 0, damp.Length);
                    }
                }
                Resources.UnloadAsset(mapElement);
            }
           
        }
    }
}
#endif