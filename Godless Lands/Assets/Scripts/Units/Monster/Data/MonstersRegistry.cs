using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Units.Monsters;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Monster
{
    [CreateAssetMenu(fileName = "MonstersRegistry", menuName = "Registry/Monsters")]
    public class MonstersRegistry : DataObjectRegistry<MonsterData>
    {

        public override void Export()
        {
            var monstersData = new List<MonsterSData>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var monster = Objects[i].ToServerData();
                monstersData.Add(monster);
            }
            ExportHelper.WriteToFile("monsters", monstersData);
        }
    }
}
