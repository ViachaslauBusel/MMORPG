#if UNITY_EDITOR
using Assets.EditorScripts;
using Helpers;
using Protocol.Data.Workbenches;
using System.Collections.Generic;

namespace OpenWorldEditor
{
    public class MachinesExport
    {
        public static void Export(MachineList machineList)
        {
            //using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/machines.dat", FileMode.Create)))
            List<WorkbenchData> workbenches = new List<WorkbenchData>();
            foreach (Machine machine in machineList.machines)
            {
                WorkbenchData workbench = new WorkbenchData()
                {
                    WorkbenchType = (WorkbenchType)machine.machineUse,
                    Position = machine.position.ToNumeric(),
                    Rotation = machine.rotation.y,
                };
                workbenches.Add(workbench);
            }
            ExportHelper.WriteToFile("machines", workbenches);
        }
    }
}
#endif