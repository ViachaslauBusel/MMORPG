using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Monsters;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Monster
{
    [CreateAssetMenu(fileName = "MonstersRegistry", menuName = "Registry/Monsters")]
    public class MonstersRegistry : DataObjectRegistry<MonsterData>
    {

        public override void Export()
        {
            var monstersData = new List<MonsterInfo>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var monster = Objects[i].ToServerData();
                monstersData.Add(monster);
            }
            ExportHelper.WriteToFile("monsters", monstersData);
        }
    }
}
