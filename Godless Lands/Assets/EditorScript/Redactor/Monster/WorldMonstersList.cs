using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterRedactor
{
    //Список все точек спавна монстров на карте
    public class WorldMonstersList : ScriptableObject
    {
        public List<WorldMonster> worldMonsters;
        public int test;
         
        public void Add(WorldMonster monster)
        {
            if (worldMonsters == null) { worldMonsters = new List<WorldMonster>(); }
              
            worldMonsters.Add(monster);
        }

        public void Remove(WorldMonster monster)
        {
            if (monster == null) return;
            worldMonsters.Remove(monster);
        }
    }
}