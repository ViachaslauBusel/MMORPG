#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MonsterRedactor {
    public class MonsterExport
    {
        public static void Export(WorldMonstersList worldMonstersList, MonstersList monstersList)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/monsters.dat", FileMode.Create)))
            {
                foreach (WorldMonster worldMonster in worldMonstersList.worldMonsters)
                {
                    Monster monster = monstersList.GetMonster(worldMonster.id);
                    stream_out.Write(worldMonster.id);//ID
                    stream_out.Write(monster.hp);
                    stream_out.Write(worldMonster.point.x);
                    stream_out.Write(worldMonster.point.y);
                    stream_out.Write(worldMonster.point.z);
                    stream_out.Write(worldMonster.radius);
                }
            }
        }
    }
}
#endif