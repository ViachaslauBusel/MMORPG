using Redactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestsRedactor
{
    public class QuestsList : ObjectList
    {
        public List<Quest> quests;

        public override void Add(object obj)
        {
            Quest _quest = obj as Quest;
            if (_quest == null) return;
            if (quests == null) quests = new List<Quest>();
            if (_quest.id == 0) _quest.id++;

            while (ConstainsKey(_quest.id)) _quest.id++;
            quests.Add(_quest);
        }

        private bool ConstainsKey(int id)
        {
            foreach (Quest _quest in quests)
            {
                if (_quest.id == id) return true;
            }
            return false;
        }
        public override int Count
        {
            get
            {
                if (quests == null) return 0;
                return quests.Count;
            }
        }
        public override System.Object this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= quests.Count) return null;
                return quests[index];
            }
        }

        public void Remove(Quest _quest)
        {
            if (_quest == null) return;
            quests.Remove(_quest);
        }

        public override void Remove(System.Object obj)
        {
            Remove(obj as Quest);
        }
        public override void RemoveAt(int index)
        {
            quests.RemoveAt(index);
        }

        public Quest GetQuest(int id)
        {
            foreach (Quest _quest in quests)
            {
                if (_quest.id == id) return _quest;
            }
            return null;
        }
    }
}