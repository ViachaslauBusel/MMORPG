using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class Quest: ScriptableObject
    {
        public int id;
        public string title;
        public List<Stage> stages;

        public Quest()
        {
            stages = new List<Stage>();
        }
    }
}