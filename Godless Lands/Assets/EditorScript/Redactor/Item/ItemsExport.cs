using Recipes;
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
                            stream_out.Write(weapon.minDamege);
                            stream_out.Write(weapon.maxDamage);
                            stream_out.Write(weapon.speed);
                            stream_out.Write(weapon.pieces.Count);
                            foreach(Piece piece in weapon.pieces)
                            {
                                stream_out.Write(piece.ID);
                                stream_out.Write(piece.count);
                            }
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