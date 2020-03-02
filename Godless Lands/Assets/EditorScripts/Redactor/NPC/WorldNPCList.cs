#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCRedactor
{
    //Список все точек спавна NPC на карте
    public class WorldNPCList : ScriptableObject
    {
        public List<WorldNPC> worldNPC;

        public void Add(WorldNPC npc)
        {
            if (worldNPC == null) { worldNPC = new List<WorldNPC>(); }

            worldNPC.Add(npc);
        }

        public void Remove(WorldNPC npc)
        {
            if (npc == null) return;
            worldNPC.Remove(npc);
        }
    }
}
#endif