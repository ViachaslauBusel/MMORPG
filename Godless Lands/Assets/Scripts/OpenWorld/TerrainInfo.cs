using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldLegacy
{

    public class TerrainInfo : MonoBehaviour {

        public Vector2Int point;


        public int xKM;
        public int yKM;
        public int xTR;
        public int yTR;

#if UNITY_EDITOR
        public Dictionary<int, GameObject> mapObjects = new Dictionary<int, GameObject>();
        public ObjectElement ObjectElement;
#endif

        public TerrainData terrainData
        {
            get
            {
                return GetComponentInChildren<Terrain>().terrainData;
            }
        }
    }
}