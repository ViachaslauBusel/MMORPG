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
        public override void Export()
        {
            //using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/machines.dat", FileMode.Create)))
            //List<WorkbenchData> workbenches = new List<WorkbenchData>();
            //foreach (Machine machine in machineList.machines)
            //{
            //    WorkbenchData workbench = new WorkbenchData()
            //    {
            //        WorkbenchType = (WorkbenchType)machine.machineUse,
            //        Position = machine.position.ToNumeric(),
            //        Rotation = machine.rotation.y,
            //    };
            //    workbenches.Add(workbench);
            //}
            //ExportHelper.WriteToFile("machines", workbenches);
        }
    }
}
