using Quests.ActiveQuestTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quests.Journal.UI
{
    public class QuestControlPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _trackQuestBut;
        private ActiveQuestsTrackerModel _activeQuestsTracker;

        [Inject]
        private void Construct(ActiveQuestsTrackerModel activeQuestsTracker)
        {
            _activeQuestsTracker = activeQuestsTracker;
        }
    }
}
