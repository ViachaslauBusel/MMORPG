using Quests.ActiveQuestTracker;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Quests.TaskTracker.UI
{
    public class QuestsTrackerWidgetUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _questPrefab;
        private QuestsModel _questsModel;
        private ActiveQuestsTrackerModel _activeQuestsTrackerModel;
        private DiContainer _diContainer;
        private Dictionary<int, QuestTrackerUI> _questUIs = new Dictionary<int, QuestTrackerUI>();


        [Inject]
        private void Construct(QuestsModel questsModel, ActiveQuestsTrackerModel activeQuestsTrackerModel, DiContainer diContainer)
        {
            _questsModel = questsModel;
            _activeQuestsTrackerModel = activeQuestsTrackerModel;
            _diContainer = diContainer;

            _activeQuestsTrackerModel.OnQuestListUpdated += UpdateQuests;
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void UpdateQuests()
        {
            //Remove quest that are no longer in the model
            for(int i = _questUIs.Values.Count - 1; i >= 0; i--)
            {
                var questUI = _questUIs.Values.ElementAt(i);
                if (!_activeQuestsTrackerModel.IsQuestTracked(questUI.QuestID))
                {
                    Debug.Log("Removing quest " + questUI.QuestID);
                    Destroy(questUI.gameObject);
                    _questUIs.Remove(questUI.QuestID);
                }
            }

            foreach (var questId in _activeQuestsTrackerModel.ActiveQuests)
            {
                Debug.Log($"Checking quest {questId}, is in UI: {_questUIs.ContainsKey(questId)}, {_questsModel.TryGetQuestById(questId, out _)}");
                if (_questUIs.ContainsKey(questId) == false && _questsModel.TryGetQuestById(questId, out var quest))
                {
                    var questUIObj = _diContainer.InstantiatePrefab(_questPrefab, _questPrefab.transform.parent);
                    questUIObj.SetActive(true);
                    var questUI = questUIObj.GetComponent<QuestTrackerUI>();
                    questUI.UpdateQuest(quest);
                    _questUIs.Add(questId, questUI);
                }
            }

            gameObject.SetActive(_questUIs.Count > 0);
        }

        private void OnDestroy()
        {
            _activeQuestsTrackerModel.OnQuestListUpdated -= UpdateQuests;
        }
    }
}
