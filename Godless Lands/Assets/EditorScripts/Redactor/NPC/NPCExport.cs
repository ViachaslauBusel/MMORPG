#if UNITY_EDITOR
using Assets.EditorScripts;
using Helpers;
using NPCs;
using Protocol.Data.NPCs;
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
            //using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/npc.dat", FileMode.Create)))
            List<NpcData> data = new List<NpcData>();
            foreach (WorldNPC worldNPC in worldNPCList.worldNPC)
            {
                NPCs.NPCPrefab npc = NPCList.GetNPC(worldNPC.id);
                NpcData npcData = new NpcData();
                npcData.id = worldNPC.id;
                npcData.spawnPoint = worldNPC.point.ToNumeric();
                npcData.spawnRotation = worldNPC.radius;
                data.Add(npcData);
            }
            ExportHelper.WriteToFile("NPCs", data);
        }
    }
}
#endif