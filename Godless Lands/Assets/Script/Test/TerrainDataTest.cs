using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainDataTest : MonoBehaviour
{
    private void OnEnable()
    {
        TerrainData terrainData = GetComponent<Terrain>().terrainData;
        if (terrainData == null) return;
        //print("Width: " + terrainData.detailWidth + " height: " + terrainData.detailHeight + " layer: " + terrainData.detailPrototypes.Length);
        //  int [,] array = terrainData.GetDetailLayer(0, 0, terrainData.detailWidth, terrainData.detailHeight, 20);
        // print(array == null);
        print("wavingGrassAmount:" + terrainData.wavingGrassAmount + "wavingGrassSpeed:" + terrainData.wavingGrassSpeed + "wavingGrassStrength:" + terrainData.wavingGrassStrength);
    }

}
