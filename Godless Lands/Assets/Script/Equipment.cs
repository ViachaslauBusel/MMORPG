using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
   public static bool Is(Item item)
    {
        if (item.use == ItemUse.Weapon) return true;
        if (item.use == ItemUse.Armor) return true;
        return false;
    }
}
