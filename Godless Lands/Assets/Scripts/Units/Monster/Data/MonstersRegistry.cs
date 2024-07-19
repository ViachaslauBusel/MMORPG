using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Walkers.Monster
{
    [CreateAssetMenu(fileName = "MonstersRegistry", menuName = "Registry/Monsters")]
    public class MonstersRegistry : DataObjectRegistry<MonsterData>
    {
    }
}
