using Assets.EditorScripts;
using NodeEditor.Data;
using Protocol.Data.Quests;
using Protocol.Data.Quests.Nodes;
using Quests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using QuestExportData = Protocol.Data.Quests.QuestData;

namespace Quests
{
    internal static class QuestExporter
    {
        internal static void Export(QuestRegistry questRegistry)
        {
            List<QuestExportData> questDatas = questRegistry.Objects
                .Select(questData => new QuestExportData(questData.ID, ExportNodes(questData.Nodes)))
                .ToList();

            ExportHelper.WriteToFile("quests", questDatas);
        }

        private static List<QuestNode> ExportNodes(IEnumerable<Node> nodes)
        {
            return nodes
                .Select(GetExportNode)
                .Where(node => node != null)
                .ToList();
        }

        private static QuestNode GetExportNode(Node node)
        {
            if (node == null)
            {
                Debug.LogError("Node is null");
                return null;
            }
            return node switch
            {
                StartNode startNode => new StartQuestNode(startNode.ID, startNode.Next.ID),
                QuestStageNode questStageNode => new Protocol.Data.Quests.Nodes.QuestStageNode(questStageNode.ID, questStageNode.NextNodeId),
                InventoryItemAvailability inventoryItemAvailability => new InventoryItemAvailabilityQuestNode(inventoryItemAvailability.ID, inventoryItemAvailability.SuccesIdNode, inventoryItemAvailability.ItemID, inventoryItemAvailability.ItemAvailableAmount),
                NearbyNPCCheck nearbyNPCCheck => new NearbyNpcCheckQuestNode(nearbyNPCCheck.ID, nearbyNPCCheck.SuccesIdNode, nearbyNPCCheck.NPCID),
                Nodes.ItemRewardNode itemAwardNode => new Protocol.Data.Quests.Nodes.ItemRewardNode(itemAwardNode.ID, itemAwardNode.SuccesIdNode, itemAwardNode.ItemID, itemAwardNode.ItemAvailableAmount),
                _ => null
            };
        }
    }
}
