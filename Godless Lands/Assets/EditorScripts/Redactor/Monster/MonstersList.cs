
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterRedactor
{
    public class MonstersList : ScriptableObject
    {
       
        public List<Monster> monsters;

        public void Add(Monster monster)
        {
            if(monsters == null)
            {
                monsters = new List<Monster>();
            }
            if (monster.id == 0) monster.id++;

            while (ConstainsKey(monster.id)) monster.id++;
            monsters.Add(monster);
        }

        public GameObject GetPrefab(int id)
        {
            foreach (Monster _monster in monsters)
            {
                if (_monster.id == id) return _monster.prefab;
            }
            return null;
        }

        public Monster GetMonster(int id)
        {
            foreach (Monster _monster in monsters)
            {
                if (_monster.id == id) return _monster;
            }
            return null;
        }

        private bool ConstainsKey(int id)
        {
            foreach (Monster _monster in monsters)
            {
                if (_monster.id == id) return true;
            }
            return false;
        }
        public void RemoveItem(Monster monster)
        {
            if (monster == null) return;
            monsters.Remove(monster);
        }
        public int Count
        {
            get { if (monsters == null) return 0; return monsters.Count; }
        }
        public Monster this[int index]
        {
            get
            {
                if (index < 0) return null;
                if (index >= monsters.Count) return null;
                return monsters[index];
            }
        }

        public List<Monster> GetList()
        {
            return monsters;
        }
    }
}
