using Inventory;
using Quests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quests.ConditionTracker.UI.Conditions
{
    internal class InventoryItemAvailabilityCondition : MonoBehaviour
    {
        private TMP_Text _text;
        private InventoryModel _inventoryModel;
        private int _trackItemId;
        private int _requiredItemCount;
        private string _conditionName;

        [Inject]
        private void Construct(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }

        public void Init(InventoryItemAvailability itemAvailability)
        {
            _text = GetComponent<TMP_Text>();
            _trackItemId = itemAvailability.ItemID;
            _requiredItemCount = itemAvailability.ItemAvailableAmount;
            _conditionName = itemAvailability.ConditionName;
            UpdateText();
            _inventoryModel.OnInventoryUpdate += UpdateText;
        }

        private void UpdateText()
        {
            _text.text = $"- ({_inventoryModel.GetItemCountByItemId(_trackItemId)}/{_requiredItemCount}) {_conditionName}";
            _text.color = _inventoryModel.GetItemCountByItemId(_trackItemId) >= _requiredItemCount ? Color.gray : Color.white;
        }

        private void OnDestroy()
        {
            _inventoryModel.OnInventoryUpdate -= UpdateText;
        }
    }
}
