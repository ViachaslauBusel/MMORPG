using Protocol.MSG.Game.Quests;
using System;

namespace Quests.ActiveQuestTracker
{
    public class QuestAutoTracker : IDisposable
    {
        private ActiveQuestsTrackerController _activeQuestsController;
        private QuestsModel _questsModel;

        public QuestAutoTracker(ActiveQuestsTrackerController activeQuestsController, QuestsModel questsModel)
        {
            _activeQuestsController = activeQuestsController;
            _questsModel = questsModel;

            _questsModel.OnQuestUpdated += OnQuestUpdated;
        }

        private void OnQuestUpdated(Quest quest, QuestSyncFlag flag)
        {
            if(flag == QuestSyncFlag.New)
            {
                _activeQuestsController.MakeActive(quest.ID);
            }
            else if (flag == QuestSyncFlag.Remove)
            {
                _activeQuestsController.MakeInactive(quest.ID);
            }
        }

        public void Dispose()
        {
            _questsModel.OnQuestUpdated -= OnQuestUpdated;
        }

    }
}
