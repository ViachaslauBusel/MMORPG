﻿
using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Units.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "NPCsRegistry", menuName = "Registry/Npcs", order = 1)]
    public class NPCsRegistry : DataObjectRegistry<NPCData>
    {
        public override void Export()
        {
            var monstersData = new List<NpcSData>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var npc = Objects[i].ToServerData();
                monstersData.Add(npc);
            }
            ExportHelper.WriteToFile("NPCs", monstersData);
        }
    }
}
