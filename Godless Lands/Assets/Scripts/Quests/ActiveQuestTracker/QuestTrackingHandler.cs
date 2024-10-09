using Protocol.MSG.Game.Quests;
using System;

namespace Quests.ActiveQuestTracker
{
    public class QuestTrackingHandler : IDisposable
    {
        private readonly ActiveQuestsTrackerPersistence _activeQuestsPersistence;
        private readonly ActiveQuestsTrackerModel _activeQuestsModel;
        private readonly QuestsModel _questsModel;

        public QuestTrackingHandler(ActiveQuestsTrackerPersistence activeQuestsController, QuestsModel questsModel, ActiveQuestsTrackerModel activeQuestsModel)
        {
            _activeQuestsPersistence = activeQuestsController;
            _questsModel = questsModel;
            _activeQuestsModel = activeQuestsModel;

            _questsModel.OnQuestUpdated += HandleQuestUpdated;
            _questsModel.OnJournalUpdated += HandleJournalUpdated;
        }

        private void HandleJournalUpdated()
        {
            _activeQuestsPersistence.ClearData();
        }

        private void HandleQuestUpdated(Quest quest, QuestSyncFlag flag)
        {
            if (flag == QuestSyncFlag.Completed)
            {
                _activeQuestsModel.RemoveQuest(quest.ID);
            }
            else if (!_activeQuestsModel.IsQuestTracked(quest.ID))
            {
                _activeQuestsModel.AddQuest(quest.ID, _activeQuestsPersistence.GetDataFor(quest.ID, true));
            }
        }

        public void Dispose()
        {
            _questsModel.OnQuestUpdated -= HandleQuestUpdated;
            _questsModel.OnJournalUpdated -= HandleJournalUpdated;
        }
    }
}
