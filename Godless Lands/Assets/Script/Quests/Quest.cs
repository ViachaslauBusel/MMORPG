
using QuestsRedactor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class Quest
    {
        public int id;
        public string title;
        public List<QuestStage> stages;

        public Quest()
        {
            stages = new List<QuestStage>();
        }

        internal QuestStage FindStage(int idStage)
        {
            foreach(QuestStage stage in stages)
            {
                if (stage.id == idStage)
                    return stage;
            }
            return null;
        }

        public bool Contains(int idStage)
        {
            foreach (QuestStage stage in stages)
            {
                if (stage.id == idStage)
                    return true;
            }
            return false;
        }
    }
}