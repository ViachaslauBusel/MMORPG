using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Quests.Journal.UI
{
    internal class QuestListRenderer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _questPrefab;
        [SerializeField]
        private Transform _content;
        private QuestJournalModel _journalModel;
        private QuestsModel _questsModel;
        private List<QuestSelectionButtonRenderer> _selectionButtons = new List<QuestSelectionButtonRenderer>();

        [Inject]
        public void Construct(QuestJournalModel journalModel, QuestsModel questsModel)
        {
            _journalModel = journalModel;
            _questsModel = questsModel;
        }

        private void Awake()
        {
            _journalModel.OnQuestSelected += OnQuestSelected;
            _questsModel.OnQuestsUpdated += RedrawQuestList;
        }

        private void RedrawQuestList()
        {
            //Remove buttons that are not in the model
            for (int i = _selectionButtons.Count - 1; i >= 0; i--)
            {
                if (!_questsModel.Quests.Any(q => _selectionButtons[i].QuestId == q.ID))
                {
                    Destroy(_selectionButtons[i].gameObject);
                    _selectionButtons.RemoveAt(i);
                }
            }

            //Add buttons that are in the model but not in the view
            foreach (var quest in _questsModel.Quests)
            {
                if (!_selectionButtons.Any(b => b.QuestId == quest.ID))
                {
                    GameObject buttonObj = Instantiate(_questPrefab, _content);
                    buttonObj.transform.SetParent(_content);
                    buttonObj.SetActive(true);
                    var button = buttonObj.GetComponent<QuestSelectionButtonRenderer>();
                    button.Init(_journalModel, quest.ID, quest.Data.Name);
                    _selectionButtons.Add(button);
                }
            }
        }

        private void OnQuestSelected(int questId)
        {
           foreach (var button in _selectionButtons)
            {
                button.IsSelected = button.QuestId == questId;
            }
        }
    }
}
