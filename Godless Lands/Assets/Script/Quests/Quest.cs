
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
    }
}