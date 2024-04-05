using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Quests.Data
{
    internal class QuestDataHolder : MonoBehaviour
    {
        [SerializeField]
        private QuestRegistry _questRegistry;

        internal QuestData GetQuestData(int questId)
        {
            return _questRegistry.GetObjectByID(questId);
        }
    }
}
