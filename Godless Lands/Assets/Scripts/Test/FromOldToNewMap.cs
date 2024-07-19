using OpenWorld;
using OpenWorld.DATA;
using OpenWorldLegacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class FromOldToNewMap : MonoBehaviour
{
    [SerializeField]
    private Map _oldMap;
    [SerializeField]
    private GameMap _newMap;

    public void Convert()
    {

        _newMap.WaterLevel = _oldMap.waterLevel;
        return;
        int totalTiles = _newMap.MapSizeKilometers * _newMap.MapSizeKilometers * _newMap.TilesPerKilometer * _newMap.TilesPerKilometer;
        int processedTiles = 0;
        for (int kmX =0; kmX < _newMap.MapSizeKilometers; kmX++)
        {
            for(int kmY = 0; kmY < _newMap.MapSizeKilometers; kmY++)
            {
                for(int tileX =0; tileX < _newMap.TilesPerKilometer; tileX++)
                {
                    for(int tileY = 0; tileY < _newMap.TilesPerKilometer; tileY++)
                    {
                        TileLocation newTileLocation = new TileLocation(_newMap);
                        newTileLocation.Xkm = kmX;
                        newTileLocation.Ykm = kmY;
                        newTileLocation.Xtr = tileX;
                        newTileLocation.Ytr = tileY;

                        MapTile newTile = AssetDatabase.LoadAssetAtPath<MapTile>(newTileLocation.Path);

                        if (newTile == null)
                        {
                            Debug.LogError($"New tile not found: {newTileLocation.Path}");
                            continue;
                        }

                        string oldTilePath = $"Assets/Resources/Island/KMBlock_{kmX}_{kmY}/TRBlock_{tileX}_{tileY}.asset";

                        MapElement oldTile = AssetDatabase.LoadAssetAtPath<MapElement>(oldTilePath);

                        if (oldTile == null)
                        {
                            Debug.LogError($"Old tile not found: {oldTilePath}");
                            continue;
                        }

                        //TerrainData tileData = newTile.TerrainData;
                        //foreach(var splat in tileData.alphamapTextures)
                        //{
                        //    string path = AssetDatabase.GetAssetPath(splat);
                        //    Debug.Log(path);
                        //    //AssetDatabase.RemoveObjectFromAsset(splat);
                        //    //AssetDatabase.AddObjectToAsset(splat, tileData);
                        //}
                        //Detach the old tile from the old map
                        var water = oldTile.waterTile;
                        if (water == null) continue;
                        AssetDatabase.RemoveObjectFromAsset(water);
                       // AssetDatabase.RemoveObjectFromAsset(newTile.TerrainData);

                        AssetDatabase.AddObjectToAsset(water, newTile);
                        newTile.SetWater(water);

                        EditorUtility.SetDirty(newTile);

                        // Update progress
                        float progress = (float)processedTiles / totalTiles;
                        EditorUtility.DisplayProgressBar("Converting", $"Converting tiles: {processedTiles} / {totalTiles}", progress);
                        processedTiles++;
                    }
                }
            }
        }
        AssetDatabase.SaveAssets();
        EditorUtility.ClearProgressBar();
    }
}
