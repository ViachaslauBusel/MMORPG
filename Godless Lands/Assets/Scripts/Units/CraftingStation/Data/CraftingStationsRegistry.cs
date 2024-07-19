using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Units.CraftingStatio.Data
{
    [CreateAssetMenu(fileName = "CraftingStationsRegistry", menuName = "Registry/CraftingStationsRegistry")]
    public class CraftingStationsRegistry : DataObjectRegistry<CraftingStationData>
    {
    }
}
