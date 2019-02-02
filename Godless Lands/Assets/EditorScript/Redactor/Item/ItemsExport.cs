using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Items
{
    public class ItemsExport
    {

        public static void Export(List<Item> items)
        {
            using (BinaryWriter stream_out = new BinaryWriter(File.Open(@"Export/items.dat", FileMode.Create)))
            {
                foreach (Item _item in items)
                {
                    stream_out.Write((int)_item.use);
                    stream_out.Write(_item.id);
                    stream_out.Write(_item.stack);
                   
                    switch (_item.use)
                    {
                        case ItemUse.Weapon:
                            WeaponItem weapon = _item.serializableObj as WeaponItem;
                            stream_out.Write(weapon.physicalAttack);
                            stream_out.Write(weapon.prickingDamage);
                            stream_out.Write(weapon.crushingDamage);
                            stream_out.Write(weapon.choppingDamage);
                            stream_out.Write(weapon.speed);
                            break;
                        case ItemUse.RestorePoints:
                            RestorePointsItem pointsItem = _item.serializableObj as RestorePointsItem;
                            stream_out.Write(pointsItem.hp);
                            stream_out.Write(pointsItem.mp);
                            stream_out.Write(pointsItem.stamina);
                            break;
                    }
                }
            }
        }
    }
}