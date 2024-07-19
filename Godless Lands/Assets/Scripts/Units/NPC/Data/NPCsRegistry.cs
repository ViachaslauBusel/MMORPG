
using ObjectRegistryEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCs
{
    [CreateAssetMenu(fileName = "NPCsRegistry", menuName = "Registry/Npcs", order = 1)]
    public class NPCsRegistry : DataObjectRegistry<NPCData>
    {
    }
}
