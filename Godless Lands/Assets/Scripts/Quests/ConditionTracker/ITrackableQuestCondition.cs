using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quests.ConditionTracker
{
    internal interface ITrackableQuestCondition
    {
        string GetInformation();
        void StartTracking();
        void StopTracking();
        void UpdateTracking(out bool isStateUpdated, out bool isConditionConfirmed);
    }
}
