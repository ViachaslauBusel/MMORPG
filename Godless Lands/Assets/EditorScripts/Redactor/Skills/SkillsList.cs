using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillsRedactor {
    public class SkillsList : ScriptableObject
    {
        public List<Skill> skills;

        public void Add(Skill skill)
        {
            if (skills == null) skills = new List<Skill>();
            if (skill.id == 0) skill.id++;

            while (ConstainsKey(skill.id)) skill.id++;
            skills.Add(skill);
        }

        public int Count
        {
            get { if (skills == null) return 0; return skills.Count; }
        }

        public Skill this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= skills.Count) return null;
                return skills[index];
            }
        }

        public void RemoveItem(Skill skill)
        {
            if (skill == null) return;
            skills.Remove(skill);
        }

        private bool ConstainsKey(int id)
        {
            foreach (Skill _skill in skills)
            {
                if (_skill.id == id) return true;
            }
            return false;
        }

        public Skill GetSkill(int id)
        {
            foreach (Skill _skill in skills)
            {
                if (_skill.id == id) return _skill;
            }
            return null;
        }
    }
}