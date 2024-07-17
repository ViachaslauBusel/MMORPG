using ObjectRegistryEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "QuestsDATA", menuName = "DATA/Quests Data", order = 51)]
    public class QuestRegistry : DataObjectRegistry<QuestData>
    {
        public override void Export()
        {
            QuestExporter.Export(this);
        }
    }
}
