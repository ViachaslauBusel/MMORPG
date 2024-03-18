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
    private InventoryWindow inventory;

    private void Awake()
    {
        inventory = GetComponentInParent<InventoryWindow>();
    }

    public void PutItem(ItemType use, Item item)
    {

        switch (use)
        {
            case ItemType.Weapon:
                weapon.PutItem(item);
                break;
            case ItemType.Pickaxe: 
                pickaxe.PutItem(item);
                break;
        }
       // inventory.Refresh();
    }

    public int GetCount(int key)
    {
        if (weapon.GetItemUID() == key) return weapon.GetCount();
        if (pickaxe.GetItemUID() == key) return pickaxe.GetCount();
        print("Key not found: " + key);
        print("Key weapon: " + weapon.GetItemUID());
        return 0;
    }

    internal Item GetItem(int key)
    {
        if (weapon.GetItemUID() == key) return weapon.GetItem();
        if (pickaxe.GetItemUID() == key) return pickaxe.GetItem();
        return null;
    }
    internal ItemCell GetItemCell(long objectID)
    {
        if (weapon.GetItemUID() == objectID) return weapon;
        if (pickaxe.GetItemUID() == objectID) return pickaxe;
        return null;
    }
}
