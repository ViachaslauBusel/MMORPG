#if UNITY_EDITOR
using DataFileProtocol.Skills;
using Helpers;
using Newtonsoft.Json;
using Protocol.Data.Monsters;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonsterRedactor {
    public class MonsterExport
    {
        public static void Export(WorldMonstersList worldMonstersList, MonstersList monstersList)
        {
           
            List<MonsterData> monsterDataList = new List<MonsterData>();
            foreach (WorldMonster worldMonster in worldMonstersList.worldMonsters)
            {
                Monster monster = monstersList.GetMonster(worldMonster.id);
                MonsterData monsterData = new MonsterData();
                monsterData.SkinID = worldMonster.id;
                monsterData.Name = monster.name;
                monsterData.HP = monster.hp;
                monsterData.SpawnPosition = worldMonster.point.ToNumeric();
                monsterData.SpawnRadius = worldMonster.radius;
                monsterDataList.Add(monsterData);
            }

            File.WriteAllText(@"Export/monsters.dat", JsonConvert.SerializeObject(monsterDataList));
        }
    }
}
#endif