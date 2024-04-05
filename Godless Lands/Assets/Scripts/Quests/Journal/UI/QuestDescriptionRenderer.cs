using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quests.Journal.UI
{
    internal class QuestDescriptionRenderer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _questTitle;
        [SerializeField]
        private TMP_Text _questDescription;
        private QuestJournalModel _questJournalModel;
        private QuestsModel _questsModel;

        [Inject]
        public void Construct(QuestJournalModel questJournalModel, QuestsModel questsModel)
        {
            _questJournalModel = questJournalModel;
            _questsModel = questsModel;
        }

        private void Awake()
        {
            ClearQuestDescription();
            _questJournalModel.OnQuestSelected += OnQuestSelected;
            _questsModel.OnQuestsUpdated += OnQuestsUpdated;
        }

        private void OnQuestsUpdated()
        {
            UpdateQuestDescription(_questsModel.Quests.FirstOrDefault(q => q.ID == _questJournalModel.CurrentSelectedQuestId));
        }

        private void OnQuestSelected(int questId)
        {
           UpdateQuestDescription(_questsModel.Quests.FirstOrDefault(q => q.ID == questId));
        }

        private void UpdateQuestDescription(Quest quest)
        {
            if (quest == null)
            {
                ClearQuestDescription();
                return;
            }
            _questTitle.text = quest.Data.Name;
            _questDescription.text = GetQuestDescription(quest);
        }

        private void ClearQuestDescription()
        {
            _questTitle.text = "";
            _questDescription.text = "";
        }

        private string GetQuestDescription(Quest quest)
        {
            QuestStageNode currentStage = quest.GetCurrentStage();
            return currentStage != null ? currentStage.Description : "No description available";
        }

        private void OnDestroy()
        {
            _questJournalModel.OnQuestSelected -= OnQuestSelected;
            _questsModel.OnQuestsUpdated -= OnQuestsUpdated;
        }
    }
}
