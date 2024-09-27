using OpenWorld.DataStore;
using UnityEngine;

namespace OpenWorld.SpawnData.CraftingStation
{
    [CreateAssetMenu(fileName = "CraftingStationWorldDataRegistry", menuName = "OpenWorld/CraftingStationWorldDataRegistry")]
    public class CraftingStationWorldDataRegistry : EntityDataStore<CraftingStationWorldData>
    {
    }
}
