using Cells;
using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryArmor : MonoBehaviour
{
    public ArmorCell weapon;
    public ArmorCell pickaxe;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
    }

    public void PutItem(ItemUse use, Item item, int key)
    {
        int quality = (item == null) ? 0 : item.count;
        switch (use)
        {
            case ItemUse.Weapon:
                weapon.PutItem(item, quality);
                weapon.SetKey(key);
                break;
            case ItemUse.Pickaxe: 
                pickaxe.PutItem(item, quality);
                pickaxe.SetKey(key);
                break;
        }
        inventory.Refresh();
    }

    public int GetCount(int key)
    {
        if (weapon.GetKey() == key) return weapon.GetCount();
        if (pickaxe.GetKey() == key) return pickaxe.GetCount();
        print("Key not found: " + key);
        print("Key weapon: " + weapon.GetKey());
        return 0;
    }

    internal Item GetItem(int key)
    {
        if (weapon.GetKey() == key) return weapon.GetItem();
        if (pickaxe.GetKey() == key) return pickaxe.GetItem();
        return null;
    }
}
