using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Units.CraftingStation;
using System.Collections.Generic;
using UnityEngine;

namespace Units.CraftingStatio.Data
{
    [CreateAssetMenu(fileName = "CraftingStationsRegistry", menuName = "Registry/CraftingStationsRegistry")]
    public class CraftingStationsRegistry : DataObjectRegistry<CraftingStationData>
    {
        public override void Export()
        {
            var stationsData = new List<CraftingStationInfo>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var station = Objects[i].ToServerData();
                stationsData.Add(station);
            }
            ExportHelper.WriteToFile("craftingStations", stationsData);
        }
    }
}
