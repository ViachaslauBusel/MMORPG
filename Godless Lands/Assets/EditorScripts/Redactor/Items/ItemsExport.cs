using Items;
using Newtonsoft.Json;
using Protocol.Data.Items;
using Recipes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ItemsRedactor
{
    public class ItemsExport
    {

        public static void Export(IEnumerable<Item> items)
        {
            List<ItemData> itemsData = new List<ItemData>();

            foreach (Item _item in items)
            {
                ItemData itemData = _item.type switch
                {
                    ItemType.Weapon => CreateWeaponData(_item),
                    ItemType.RestorePoints => CreateRestorePointsItemData(_item),
                    _ => new ItemData(_item.id, _item.stack, _item.weight)
                };
                itemsData.Add(itemData);
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            File.WriteAllText(@"Export/items.dat", JsonConvert.SerializeObject(itemsData, settings));
        }

        private static ItemData CreateRestorePointsItemData(Item item)
        {
            if (item.serializableObj is RestorePointsItem restorePoints)
            {
                return new RestorePointsItemData(item.id, item.stack, item.weight, restorePoints.hp, restorePoints.mp, restorePoints.stamina);
            }
            throw new ArgumentException("Item is not a restore points item");
        }

        private static ItemData CreateWeaponData(Item item)
        {
            if (item.serializableObj is WeaponItem weapon)
            {
                return new WeaponItemData(item.id, item.stack, item.weight, weapon.minDamege, weapon.maxDamage, (int)(weapon.speed * 1_000));
            }
           throw new ArgumentException("Item is not a weapon");
        }
    }
}