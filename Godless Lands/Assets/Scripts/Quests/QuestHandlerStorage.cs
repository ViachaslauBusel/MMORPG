using NodeEditor.Data;
using Quests.Nodes;
using Quests.Nodes.Handlers;
using System;
using System.Collections.Generic;
using Zenject;

namespace Quests
{
    public class QuestHandlerStorage : IInitializable
    {
        private DiContainer _container;
        private Dictionary<Type, INodeHandler> _handlers = new Dictionary<Type, INodeHandler>();

        public QuestHandlerStorage(DiContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _handlers.Add(typeof(InventoryItemRequirementNode), _container.Resolve<InventoryItemAvailabilityHandler>());
        }

        public INodeHandler GetHandler(Type type)
        {
            return _handlers[type];
        }

        internal bool GetHandler(Node node, out INodeHandler handler)
        {
            return _handlers.TryGetValue(node.GetType(), out handler);
        }
    }
}
