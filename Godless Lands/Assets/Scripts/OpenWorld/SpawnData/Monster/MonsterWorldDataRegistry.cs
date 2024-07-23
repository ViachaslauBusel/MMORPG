using OpenWorld.DataStore;
using UnityEngine;

namespace OpenWorld.SpawnData.Monster
{
    [CreateAssetMenu(fileName = "MonsterWorldDataRegistry", menuName = "OpenWorld/MonsterWorldDataRegistry")]
    public class MonsterWorldDataRegistry : EntityDataStore<MonsterWorldData>
    {
    }
}
