using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenWorldLegacy
{
    public class Map : ScriptableObject
    {

        public int mapSize;
        public int blocksCount;
        public string mapName;
        public float blockSize;
        public Vector2 startWorld;
        public float height;
        public float setHeight;
        public readonly float sizeKMBlock = 1000.0f;
        public int heightmapResolution = 129;
        public float waterLevel;
    }
}