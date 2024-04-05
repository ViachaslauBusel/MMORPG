using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quests.ConditionTracker.UI
{
    internal class QuestsTrackerScreenUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _questPrefab;
        private QuestsModel _questsModel;
        private DiContainer _diContainer;
        private Dictionary<int, QuestTrackerUI> _questUIs = new Dictionary<int, QuestTrackerUI>();


        [Inject]
        public void Construct(QuestsModel questsModel, DiContainer diContainer)
        {
            _questsModel = questsModel;
            _diContainer = diContainer;

            _questsModel.OnQuestsUpdated += UpdateQuests;
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void UpdateQuests()
        {
            //Remove quest that are no longer in the model
            foreach (var questUI in _questUIs.Values.ToArray())
            {
                if (!_questsModel.Quests.Any(q => q.ID == questUI.QuestID))
                {
                    Destroy(questUI.gameObject);
                    _questUIs.Remove(questUI.QuestID);
                }
            }

            foreach (var quest in _questsModel.Quests)
            {
                if(quest.IsCompleted)
                {
                    if(_questUIs.ContainsKey(quest.ID))
                    {
                        Destroy(_questUIs[quest.ID].gameObject);
                        _questUIs.Remove(quest.ID);
                    }
                    continue;
                }

                if(_questUIs.ContainsKey(quest.ID))
                {
                    _questUIs[quest.ID].UpdateQuest(quest);
                }
                else
                {
                    var questUIObj = _diContainer.InstantiatePrefab(_questPrefab, transform);
                    questUIObj.SetActive(true);
                    var questUI = questUIObj.GetComponent<QuestTrackerUI>();
                    questUI.UpdateQuest(quest);
                    _questUIs.Add(quest.ID, questUI);
                }
            }

            gameObject.SetActive(_questsModel.Quests.Count > 0);
        }

        private void OnDestroy()
        {
            _questsModel.OnQuestsUpdated -= UpdateQuests;
        }
    }
}
