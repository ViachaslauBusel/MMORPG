#if UNITY_EDITOR
using NPCs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCRedactor
{
    public class NPCEditor : ScriptableObject
    {
        public NPCs.NPCPrefab npc;
    }
}
#endif