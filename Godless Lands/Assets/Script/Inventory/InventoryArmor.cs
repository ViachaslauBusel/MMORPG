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

    public void PutItem(ItemUse use, Item item)
    {
        int quality = (item == null) ? 0 : item.count;
        int objectID = (item == null) ? 0 : item.objectID;
        switch (use)
        {
            case ItemUse.Weapon:
                weapon.PutItem(item, quality);
                weapon.SetObjectID(objectID);
                break;
            case ItemUse.Pickaxe: 
                pickaxe.PutItem(item, quality);
                pickaxe.SetObjectID(objectID);
                break;
        }
        inventory.Refresh();
    }

    public int GetCount(int key)
    {
        if (weapon.GetObjectID() == key) return weapon.GetCount();
        if (pickaxe.GetObjectID() == key) return pickaxe.GetCount();
        print("Key not found: " + key);
        print("Key weapon: " + weapon.GetObjectID());
        return 0;
    }

    internal Item GetItem(int key)
    {
        if (weapon.GetObjectID() == key) return weapon.GetItem();
        if (pickaxe.GetObjectID() == key) return pickaxe.GetItem();
        return null;
    }
}
