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

        public static void Export(IEnumerable<Items.ItemData> items)
        {
            List<Protocol.Data.Items.ItemData> itemsData = new List<Protocol.Data.Items.ItemData>();

            foreach (Items.ItemData _item in items)
            {
                Protocol.Data.Items.ItemData itemData = _item.type switch
                {
                    ItemType.Weapon => CreateWeaponData(_item),
                    ItemType.RestorePoints => CreateRestorePointsItemData(_item),
                    ItemType.Pickaxe => CreatePickaxeItemData(_item),
                    _ => new Protocol.Data.Items.ItemData(_item.id, _item.stack, _item.weight)
                };
                itemsData.Add(itemData);
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            File.WriteAllText(@"Export/items.dat", JsonConvert.SerializeObject(itemsData, settings));
        }

        private static Protocol.Data.Items.ItemData CreatePickaxeItemData(Items.ItemData item)
        {
            return new PickaxeItemData(item.id, item.stack, item.weight);
        }

        private static Protocol.Data.Items.ItemData CreateRestorePointsItemData(Items.ItemData item)
        {
            if (item.serializableObj is RestorePointsItem restorePoints)
            {
                return new RestorePointsItemData(item.id, item.stack, item.weight, restorePoints.hp, restorePoints.mp, restorePoints.stamina);
            }
            throw new ArgumentException("Item is not a restore points item");
        }

        private static Protocol.Data.Items.ItemData CreateWeaponData(Items.ItemData item)
        {
            if (item.serializableObj is WeaponItem weapon)
            {
                return new WeaponItemData(item.id, item.stack, item.weight, weapon.minDamege, weapon.maxDamage, (int)(weapon.speed * 1_000));
            }
           throw new ArgumentException("Item is not a weapon");
        }
    }
}