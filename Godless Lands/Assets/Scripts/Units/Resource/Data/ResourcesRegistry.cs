using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Resources;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Resource.Data
{
    [CreateAssetMenu(fileName = "ResourcesRegistry", menuName = "Registry/ResourcesRegistry")]
    public class ResourcesRegistry : DataObjectRegistry<ResourceHarvestData>
    {
        public override void Export()
        {
            var monstersData = new List<ResourceInfo>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var monster = Objects[i].ToServerData();
                monstersData.Add(monster);
            }
            ExportHelper.WriteToFile("resources", monstersData);
        }
    }
}
