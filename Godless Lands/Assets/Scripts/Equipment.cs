using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
   public static bool Is(Item item)
    {
        if (item.type == ItemType.Weapon) return true;
        if (item.type == ItemType.Armor) return true;
        return false;
    }
}
