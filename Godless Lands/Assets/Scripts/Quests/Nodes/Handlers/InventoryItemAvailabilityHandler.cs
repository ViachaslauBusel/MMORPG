using Inventory;
using NodeEditor.Data;
using System;

namespace Quests.Nodes.Handlers
{
    public class InventoryItemAvailabilityHandler : INodeHandler, IDisposable
    {
        private InventoryModel _inventoryModel;

        public event Action OnTaskProgressChanged;

        public InventoryItemAvailabilityHandler(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;

            _inventoryModel.OnInventoryUpdated += OnInventoryUpdated;
        }

        private void OnInventoryUpdated()
        {
            OnTaskProgressChanged?.Invoke();
        }

        public Node GetNextNode(Node node)
        {
            if (node is InventoryItemRequirementNode inventoryItemAvailability)
            {
                return inventoryItemAvailability.NextNode;
            }
            return null;
        }

        public string GetDescription(Node node)
        {
            if (node is InventoryItemRequirementNode inventoryItemAvailability)
            {
                return inventoryItemAvailability.ConditionName;
            }
            return string.Empty;
        }

        public string GetProgress(Node node)
        {
            if (node is InventoryItemRequirementNode inventoryItemAvailability)
            {
                var itemCount = _inventoryModel.GetItemCountByItemId(inventoryItemAvailability.RequiredItemId);
                return $"{itemCount}/{inventoryItemAvailability.RequiredItemAmount}";
            }
            return string.Empty;
        }

        public void Dispose()
        {
            _inventoryModel.OnInventoryUpdated -= OnInventoryUpdated;
        }
    }
}
