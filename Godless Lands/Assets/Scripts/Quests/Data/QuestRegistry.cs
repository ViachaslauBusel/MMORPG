using Helpers;
using NodeEditor.Data;
using ObjectRegistryEditor;
using Protocol.Data.Quests;
using Quests.Export;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "QuestsDATA", menuName = "DATA/Quests Data", order = 51)]
    public class QuestRegistry : DataObjectRegistry<QuestData>
    {
        public override void Export()
        {
            List<QuestSData> questDatas = Objects
              .Select(questData => new QuestSData(questData.ID, ExportNodes(questData.Nodes)))
              .ToList();

            ExportHelper.WriteToFile("quests", questDatas);
        }

        private static List<QuestSNode> ExportNodes(IEnumerable<Node> nodes)
        {
            return nodes.Select(n => n as IExportableNode)
                        .Where(n => n != null)
                        .Select(n => n.ToServerData())
                        .ToList();
        }
    }
}
