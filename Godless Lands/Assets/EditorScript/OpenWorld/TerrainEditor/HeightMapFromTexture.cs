#if UNITY_EDITOR
using OpenWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenWorldEditor
{
    public class HeightMapFromTexture
    {
        public static void Apply(Map map, HeightMapSetting heightMapSetting)
        {
            int heightMapPoint =  map.mapSize * map.blocksCount * (map.heightmapResolution - 1) + 1;
            Color[] mapColors = heightMapSetting.texture.GetPixels(0, 0, heightMapSetting.texture.width, heightMapSetting.texture.height);


            float[] grayScale = new float[heightMapPoint * heightMapPoint];

            float pixelX = heightMapSetting.texture.width / (float)heightMapPoint;
            float pixelY = heightMapSetting.texture.height / (float)heightMapPoint;

            for (int y = 0; y < heightMapPoint; y++)
            {
                for (int x = 0; x < heightMapPoint; x++)
                {
                    float xSmooth = x * pixelX;
                    float ySmooth = y * pixelY;
                    int xPos = (int)xSmooth;
                    int yPos = (int)ySmooth;
                    xSmooth -= xPos;
                    ySmooth -= yPos;

                    int cColor = xPos + yPos * heightMapSetting.texture.width;
                    int xColor = (xPos + 1) + yPos * heightMapSetting.texture.width;
                    int yColor = xPos + (yPos + 1) * heightMapSetting.texture.width;
//if(cColor >= mapColors.Length) cColor = mapColors.Length - 1;
                    if (xColor >= mapColors.Length) xColor = mapColors.Length - 1;
                    if (yColor >= mapColors.Length) yColor = mapColors.Length - 1;
                    grayScale[x + y * heightMapPoint] = Color.Lerp(Color.Lerp(mapColors[cColor], mapColors[xColor], xSmooth), Color.Lerp(mapColors[cColor], mapColors[yColor], ySmooth), 0.5f).grayscale;
                }
            }

            float setHeight = heightMapSetting.maxHeight - heightMapSetting.minHeight;

            for (int yKM = 0; yKM < map.mapSize; yKM++)
            {
                for (int xKM = 0; xKM < map.mapSize; xKM++)
                {


                    for (int y = 0; y < map.blocksCount; y++)
                    {
                        for (int x = 0; x < map.blocksCount; x++)
                        {
                            string path = map.mapName + "/KMBlock_" + xKM + '_' + yKM + "/TRBlock_" + x + '_' + y;

                            MapElement resource = Resources.Load<MapElement>(path);
                            TerrainData terrainData = (resource as MapElement).terrainData;

                            int xStart = (xKM * map.blocksCount + x) * (map.heightmapResolution - 1);
                            int yStart = (yKM * map.blocksCount + y) * (map.heightmapResolution - 1);
                            SetHeight(terrainData, xStart, yStart, heightMapSetting.minHeight, setHeight, map.height, grayScale, heightMapPoint);
                        }
                    }


                }
            }

        }


        private static void SetHeight(TerrainData terrainData, int xStart, int yStart, float minHeight, float setHeight, float mapHeight, float[] grayScale, int heightMapPoint)
        {
            float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
         

            for (int y = 0; y < heights.GetLength(1); y++)
            {
                for (int x = 0; x < heights.GetLength(0); x++)
                {
                    float scale = grayScale[xStart + x + ((yStart + y) * heightMapPoint)];
                    heights[y, x] = (minHeight + setHeight * scale)/mapHeight;
                }
            }

            terrainData.SetHeights(0, 0, heights);

        }


    }
}
#endif