using NodeEditor.Data;
using Quests.Nodes;
using Quests.Nodes.Handlers;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestNodeCoordinator
    {
        private QuestHandlerStorage _questHandlerStorage;

        public QuestNodeCoordinator(QuestHandlerStorage questHandlerStorage)
        {
            _questHandlerStorage = questHandlerStorage;
        }

        public bool GetNextQuestStage(Node questNode, out List<NodeTask> tasks, out QuestStageNode nextStage)
        {
            if(questNode == null)
            {
                tasks = null;
                nextStage = null;
                return false;
            }
            Node nextNode = GetNextNode(questNode);
            return ProcessTask(nextNode, out tasks, out nextStage);
        }

        private Node GetNextNode(Node questNode)
        {
            return questNode switch
            {
                QuestStageNode stage => stage.NextNode,
                StartNode startNode => startNode.Next,
                _ => null
            };
        }

        private bool ProcessTask(Node nextNode, out List<NodeTask> tasks, out QuestStageNode nextStage)
        {
            tasks = new List<NodeTask>();
            nextStage = ProcessNode(nextNode, tasks);
            return nextStage != null;
        }


        private QuestStageNode ProcessNode(Node node, List<NodeTask> handlers)
        {
            if (node == null) return null;

            if (_questHandlerStorage.GetHandler(node, out INodeHandler handler))
            {
                handlers.Add(new NodeTask(handler, node));
                return ProcessNode(handler.GetNextNode(node), handlers);
            }

            switch (node)
            {
                case QuestStageNode questStage: return questStage;
                case StartNode startNode:
                    return ProcessNode(startNode.Next, handlers);
                case ItemRewardNode itemRewardNode:
                    return ProcessNode(itemRewardNode.NextNode, handlers);
                case NearbyNPCValidatorNode nearbyNPCCheck:
                    return ProcessNode(nearbyNPCCheck.NextNode, handlers);
                default:
                    Debug.LogError("Unknown node type: " + node.GetType());
                    return null;
            }
        }
    }
}
