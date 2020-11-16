using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class Stage
    {
        //ИД этого звена(уровня)
        public int id;
        public string title = "no title";
        public string descripton = "empty";
        public List<int> answers;

        public Stage()
        {
            answers = new List<int>();
        }
    }
}