using Cells;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryArmor : MonoBehaviour
{
    public ArmorCell weapon;
    public ArmorCell pickaxe;

    public void PutItem(ItemUse use, Item item)
    {
        switch (use)
        {
            case ItemUse.Weapon:
                weapon.PutItem(item, 1);
                break;
            case ItemUse.Pickaxe:
                pickaxe.PutItem(item, 1);
                break;
        }
    }
}
