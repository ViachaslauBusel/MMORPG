using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quests.Journal.UI
{
    internal class QuestSelectionButtonRenderer : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        Text _questNameText;
        private QuestJournalModel _journalModel;
        private int _questId;

        public bool IsSelected
        {
            get => !_button.interactable;
            set
            {
                _button.interactable = !value;
            }
        }

        public int QuestId => _questId;

        public void Init(QuestJournalModel questJournalModel, int questId, string questName)
        {
            _journalModel = questJournalModel;
            _questId = questId;
            _questNameText.text = questName;
            _button.onClick.AddListener(() => _journalModel.SetCurrentSelectedQuestId(questId));
        }
    }
}
