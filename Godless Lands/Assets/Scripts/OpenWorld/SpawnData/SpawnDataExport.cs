#if UNITY_EDITOR
using Helpers;
using Newtonsoft.Json;
using OpenWorld.DATA;
using OpenWorld.Utilities;
using Protocol.Data.SpawnData;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OpenWorld.SpawnData
{
    public class SpawnDataExport : IMapUtilityForMapEntity
    {
        private List<SpawnUnitPointData> _spawnPoints = new List<SpawnUnitPointData>();
        public string Name => "Export spawn.dat";

        public void BeginExecution(GameMap map)
        {
            _spawnPoints.Clear();
        }

        public bool Execute(MapEntity mapEntity)
        {
            var identofier = ((GameObject)mapEntity.Prefab.editorAsset).GetComponent<ISpawnPoint>();
            if (identofier != null)
            {
                if (identofier is ISpawnPoint spawnData)
                {
                    identofier.LoadData(mapEntity.ID, true);
                    var spawnPoint = new SpawnUnitPointData
                    {
                        UnitType = spawnData.UnitType,
                        UnitID = spawnData.UnitID,
                        SpawnType = (Protocol.Data.SpawnData.SpawnPointType)spawnData.SpawnPointType,
                        Position = mapEntity.Position.ToNumeric(),
                        SpawnRadius = spawnData.SpawnPointType == SpawnPointType.Point ? mapEntity.Rotation.eulerAngles.y : spawnData.SpawnRadius,
                        MinSpawnTime = (int)(spawnData.MinSpawnTime * 1000f),
                        MaxSpawnTime = (int)(spawnData.MaxSpawnTime * 1000f)
                    };
                    _spawnPoints.Add(spawnPoint);
                }

            }
            return false;
        }

        public void EndExecution(bool success)
        {
            if (success)
            {
                ExportHelper.WriteToFile("spawn", _spawnPoints);
                EditorUtility.DisplayDialog("Export spawn.dat", "spawn.dat exported successfully", "Ok");
            }
            else
            {
                EditorUtility.DisplayDialog("Export spawn.dat", "Failed to export spawn.dat", "Ok");
            }
        }
    }
}
#endif