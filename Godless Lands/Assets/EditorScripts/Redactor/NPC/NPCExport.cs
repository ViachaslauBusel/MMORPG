#if UNITY_EDITOR
using NPCs;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NPCRedactor
{
    public class NPCExport : MonoBehaviour
    {
        public static void Export(WorldNPCList worldNPCList, NPCList NPCList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/npc.dat", FileMode.Create)))
            {
                foreach (WorldNPC worldNPC in worldNPCList.worldNPC)
                {
                    NPCs.NPCPrefab npc = NPCList.GetNPC(worldNPC.id);
                    stream_out.Write(worldNPC.id);//ID
                    stream_out.Write(worldNPC.point.x);
                    stream_out.Write(worldNPC.point.y);
                    stream_out.Write(worldNPC.point.z);
                    stream_out.Write(worldNPC.radius);
                }
            }
        }
    }
}
#endif