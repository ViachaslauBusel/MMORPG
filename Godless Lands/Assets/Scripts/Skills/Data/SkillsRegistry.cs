using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Skills.Data
{
    [CreateAssetMenu(fileName = "SkillRegistry", menuName = "Registry/Skills")]
    public class SkillsRegistry : DataObjectRegistry<SkillData>
    {
    }
}
