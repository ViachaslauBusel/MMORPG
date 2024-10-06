using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.ActiveQuestTracker
{
    public class ActiveQuestsTrackerController
    {
        private ActiveQuestsTrackerModel _model;

        public ActiveQuestsTrackerController(ActiveQuestsTrackerModel model)
        {
            _model = model;
        }

        public void MakeActive(int questId)
        {
            _model.AddQuest(questId);
        }

        public void MakeInactive(int questId)
        {
            _model.RemoveQuest(questId);
        }
    }
}
