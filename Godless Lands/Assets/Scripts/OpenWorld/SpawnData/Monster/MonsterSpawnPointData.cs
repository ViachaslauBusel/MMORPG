#if UNITY_EDITOR
using Units.Monster;
using UnityEngine;

namespace OpenWorld.SpawnData.Monster
{
    [ExecuteInEditMode]
    public class MonsterSpawnPointData : SpawnPointDataSync<MonsterWorldData, MonsterData>
    {
    }
}
#endif