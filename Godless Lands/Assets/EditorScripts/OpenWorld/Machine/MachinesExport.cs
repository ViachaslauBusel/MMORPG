#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OpenWorldEditor
{
    public class MachinesExport
    {
        public static void Export(MachineList machineList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/machines.dat", FileMode.Create)))
            {
                foreach (Machine machine in machineList.machines)
                {
                    stream_out.Write((int)machine.machineUse);
                    stream_out.Write(machine.position.x);
                    stream_out.Write(machine.position.y);
                    stream_out.Write(machine.position.z);
                    stream_out.Write(machine.rotation.x);
                    stream_out.Write(machine.rotation.y);
                    stream_out.Write(machine.rotation.z);
                 /*   stream_out.Write(machine.scale.x);
                    stream_out.Write(machine.scale.y);
                    stream_out.Write(machine.scale.z); 
                    */
                }
            }
        }
    }
}
#endif