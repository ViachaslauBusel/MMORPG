using System;
using UnityEngine;

namespace OpenWorld.SpawnData
{
    [Serializable]
    public class SpawnPointData
    {
        public ScriptableObject DataObject;
        public SpawnPointType SpawnPointType;
        public float SpawnPointRadius;
        public float MinSpawnTime;
        public float MaxSpawnTime;
    }
}
