using Helpers;
using ObjectRegistryEditor;
using Protocol.Data.Items.Types;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemsRegistry", menuName = "Registry/Items", order = 1)]
    public class ItemsRegistry : DataObjectRegistry<ItemData>
    {
        public override void Export()
        {
            var itemsData = new List<ItemSData>();
            for (int i = 0; i < Objects.Count; i++)
            {
                var item = Objects[i].ToServerData();
                itemsData.Add(item);
            }
            ExportHelper.WriteToFile("items", itemsData);
        }
    }
}