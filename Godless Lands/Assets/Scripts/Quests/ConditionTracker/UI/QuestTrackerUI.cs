using NodeEditor.Data;
using Quests.ConditionTracker.UI.Conditions;
using Quests.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quests.ConditionTracker.UI
{
    internal class QuestTrackerUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _questName;
        [SerializeField]
        private GameObject _questCanditionPrefab;
        private List<GameObject> _questConditions = new List<GameObject>(); 
        private DiContainer _diContainer;

        public int QuestID { get; private set; }

        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        internal void UpdateQuest(Quest quest)
        {
            QuestID = quest.ID;
            _questName.text = quest.Data.Name;

            foreach(var con in _questConditions)
            {
                Destroy(con);
            }
            _questConditions.Clear();

            QuestStageNode currentNode = quest.GetCurrentStage();

            if (currentNode == null) return;

            AddCondiciton(currentNode.NextNode);

            gameObject.SetActive(_questConditions.Count > 0);
        }

        private void AddCondiciton(Node nextNode)
        {
            switch (nextNode)
            {
                case QuestStageNode: break;
                case ToStartNode: break;
                case InventoryItemAvailability inventoryItemAvailability:
                    var condition = _diContainer.InstantiatePrefab(_questCanditionPrefab, transform);
                    condition.SetActive(true);
                    var conditionScript = _diContainer.InstantiateComponent<InventoryItemAvailabilityCondition>(condition);
                    conditionScript.Init(inventoryItemAvailability);
                    _questConditions.Add(condition);
                    AddCondiciton(inventoryItemAvailability.NextNode);
                    break;
               
                default:
                    if (nextNode is IHaveNextNode haveNextNode)
                    {
                        AddCondiciton(haveNextNode.NextNode);
                    }
                    else Debug.LogWarning($"Unknown node type:{nextNode.GetType()}");

                    break;
            }
        }
    }
}
