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

public class FromOldToNewMap : MonoBehaviour
{
    [SerializeField]
    private Map _oldMap;
    [SerializeField]
    private GameMap _newMap;

    public void Convert()
    {
        for(int kmX =0; kmX < _newMap.MapSizeKilometers; kmX++)
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

                        string oldTilePath = $"Resources/Island/KMBlock_{kmX}_{kmY}/TRBlock_{tileX}_{tileY}.asset";

                        MapElement oldTile = AssetDatabase.LoadAssetAtPath<MapElement>(oldTilePath);

                        if (oldTile == null)
                        {
                            Debug.LogError($"Old tile not found: {oldTilePath}");
                            continue;
                        }

                        TerrainData tileData = oldTile.terrainData;
                        //Detach the old tile from the old map
                        AssetDatabase.RemoveObjectFromAsset(tileData);
                        AssetDatabase.RemoveObjectFromAsset(newTile.TerrainData);

                        newTile.SetTerrainData(tileData);
                        AssetDatabase.AddObjectToAsset(newTile.TerrainData, _newMap);

                    }
                }
            }
        }
    }
}
