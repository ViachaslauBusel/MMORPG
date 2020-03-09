using Items;
using RUCP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public static class ItemNetwork
    {
        public static Item ReadItem(this NetworkWriter nw)
        {
            Item item = Inventory.CreateItem(nw.ReadInt());//Создаем предмет по ид предмета, если такого предмета нет пустой итем обьект


            if (!item.IsExist()) return item;

            item.objectID = nw.ReadInt();//Уникальный ИД обьекта
            item.count = nw.ReadInt();//Количество либо качества

            item.type = (ItemType)nw.ReadInt();
            switch (item.type)
            {
                case ItemType.Weapon:
                    item.enchant_level = nw.ReadInt();
                    item.durability = nw.ReadInt();
                    item.maxDurability = nw.ReadInt();
                    break;
            }

            return item;
        }
    }
}