﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quests.TaskTracker.UI
{
    internal class QuestTrackerUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _questName;
        [SerializeField]
        private GameObject _questCanditionPrefab;
        private List<GameObject> _questConditions = new List<GameObject>(); 
        private DiContainer _diContainer;
        private QuestNodeCoordinator _questNodeCoordinator;

        public int QuestID { get; private set; }

        [Inject]
        public void Construct(DiContainer diContainer, QuestNodeCoordinator questNodeCoordinator)
        {
            _diContainer = diContainer;
            _questNodeCoordinator = questNodeCoordinator;
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

            //If there is no next stage, then we dont show any tasks
            if (_questNodeCoordinator.GetNextQuestStage(quest.GetCurrentStage(), out var task, out _) == false) return;

            foreach (var t in task)
            {
                AddCondiciton(t);
            }

            gameObject.SetActive(_questConditions.Count > 0);
        }

        private void AddCondiciton(NodeTask task)
        {
            var condition = _diContainer.InstantiatePrefab(_questCanditionPrefab, _questCanditionPrefab.transform.parent);
            condition.SetActive(true);
            var conditionScript = condition.GetComponent<QuestConditionTrackerUI>();
            conditionScript.Init(task);
            _questConditions.Add(condition);
        }
    }
}
