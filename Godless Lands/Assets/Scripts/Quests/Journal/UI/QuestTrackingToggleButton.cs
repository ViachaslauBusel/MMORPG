using Quests.ActiveQuestTracker;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quests.Journal.UI
{
    public class QuestTrackingToggleButton : MonoBehaviour
    {
        private Button _trackQuestBut;
        private TMP_Text _trackQuestButTxt;
        private ActiveQuestsTrackerModel _activeQuestsTracker;
        private QuestJournalModel _questJournalModel;

        [Inject]
        private void Construct(ActiveQuestsTrackerModel activeQuestsTracker, QuestJournalModel questJournalModel)
        {
            _activeQuestsTracker = activeQuestsTracker;
            _questJournalModel = questJournalModel;
        }

        private void Awake()
        {
            _trackQuestBut = GetComponent<Button>();
            _trackQuestButTxt = GetComponentInChildren<TMP_Text>();

            _trackQuestBut.onClick.AddListener(TrackQuest);
            UpdateTrackQuestButUi();

            _questJournalModel.OnQuestSelected += QuestJournalModelOnOnQuestSelected;
        }

        private void QuestJournalModelOnOnQuestSelected(int obj)
        {
            UpdateTrackQuestButUi();
        }

        private void UpdateTrackQuestButUi()
        {
            if (_activeQuestsTracker.IsQuestTracked(_questJournalModel.CurrentSelectedQuestId))
            {
                _trackQuestButTxt.text = "Untrack";
            }
            else
            {
                _trackQuestButTxt.text = "Track";
            }
        }

        private void TrackQuest()
        {
            _activeQuestsTracker.SetQuestActive(_questJournalModel.CurrentSelectedQuestId, !_activeQuestsTracker.IsQuestTracked(_questJournalModel.CurrentSelectedQuestId));
            UpdateTrackQuestButUi();
        }

        private void OnDestroy()
        {
            _questJournalModel.OnQuestSelected -= QuestJournalModelOnOnQuestSelected;
        }
    }
}
