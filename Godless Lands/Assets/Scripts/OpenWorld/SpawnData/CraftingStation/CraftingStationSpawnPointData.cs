#if UNITY_EDITOR
using OpenWorld.SpawnData;
using OpenWorld.SpawnData.CraftingStation;
using OpenWorld.SpawnData.Monster;
using Units.CraftingStatio.Data;
using Units.Monster;
using UnityEngine;

namespace OpenWorld.SpawnData.CraftingStation
{
    [ExecuteInEditMode]
    public class CraftingStationSpawnPointData : SpawnPointDataSync<CraftingStationWorldData, CraftingStationData>
    {
    }
}
#endif
