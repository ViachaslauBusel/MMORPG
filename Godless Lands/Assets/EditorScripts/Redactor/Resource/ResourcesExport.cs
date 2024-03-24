#if UNITY_EDITOR
using Assets.EditorScripts;
using Helpers;
using Protocol.Data.Items;
using Protocol.Data.MiningStone;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Resource
{
    public class ResourcesExport
    {
        public static void Export(WorldResourcesList worldResourcesList, ResourceList resourceList)
        {
            List<ResourceMiningStoneData> data = new List<ResourceMiningStoneData>();
            foreach (WorldFabric worldFabric in worldResourcesList.worldResources)
            {
                Fabric fabric = resourceList.GetFabric(worldFabric.id);
                if (fabric == null)
                {
                    Debug.LogError("Fabric not found");
                    continue;
                }
                ResourceMiningStoneData miningStoneData = new ResourceMiningStoneData()
                {
                    ID = worldFabric.id,
                    SpawnPoint = worldFabric.point.ToNumeric(),
                    SpawnRadius = worldFabric.radius,
                    StartSpawnTime = (int)(fabric.startSpawn * 1000),
                    TimeSpawn = (int)(fabric.timeSpawn * 1000),
                    Profesion = (int)fabric.profession,
                    Exp = fabric.exp,
                    Stamina = fabric.stamina,
                    Drops = GetDrops(fabric.drops, fabric)
                };
                data.Add(miningStoneData);
            }
            ExportHelper.Write("miningStones", data);
        }

        private static List<DropItemData> GetDrops(in List<Drop> drops, Fabric fabric)
        {
            //Drop
            List<DropItemData> dropItems = new List<DropItemData>();
            foreach (Drop drop in fabric.drops)
            {
                DropItemData dropItem = new DropItemData()
                {
                    ItemID = drop.itemID,
                    Amount = 1,
                    Chance = drop.chance
                };
                dropItems.Add(dropItem);
            }
            return dropItems;
        }
    }
}
#endif