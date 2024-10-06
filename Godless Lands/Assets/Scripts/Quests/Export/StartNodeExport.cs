using NodeEditor.Data;
using Protocol.Data.Quests;
using Protocol.Data.Quests.Nodes;

namespace Quests.Export
{
    internal class StartNodeExport : IExportableNode
    {
        private StartNode _startNode;

        public StartNodeExport(StartNode startNode)
        {
            _startNode = startNode;
        }

        public QuestSNode ToServerData()
        {
            return new StartQuestSNode(_startNode.ID, _startNode.Next?.ID ?? 0);
        }
    }
}