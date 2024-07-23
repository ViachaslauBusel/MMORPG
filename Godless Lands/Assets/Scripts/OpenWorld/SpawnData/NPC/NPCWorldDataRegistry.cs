using OpenWorld.DataStore;
using UnityEngine;

namespace OpenWorld.SpawnData.NPC
{
    [CreateAssetMenu(fileName = "MonsterWorldDataRegistry", menuName = "OpenWorld/NpcWorldDataRegistry")]
    public class NPCWorldDataRegistry : EntityDataStore<NpcWorldData>
    {
    }
}
