using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
   public static bool Is(Item item)
    {
        if (item.Data.type == ItemType.Weapon) return true;
        if (item.Data.type == ItemType.Armor) return true;
        return false;
    }
}
