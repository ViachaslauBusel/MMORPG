using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using Cells;
using Items;

public class Armor : MonoBehaviour {

    //public CellArmor head;
    public ArmorCell cell_weapon;
    public CharacterArmor characterArmor;
    private ItemsList itemsList;


    private void Awake()
    {

        itemsList = GetComponent<Inventory>().itemsList;
        RegisteredTypes.RegisterTypes(Types.UpdateArmor, UpdateArmor);
    }

    

    private void UpdateArmor(NetworkWriter nw)
    {
        ItemUse part = (ItemUse)nw.ReadInt();
        int id_item = nw.ReadInt();

        switch (part)
        {
            case ItemUse.Weapon:
                Item _item = itemsList.GetItem(id_item);
                cell_weapon.PutItem(_item);
                characterArmor.PutOnWeapon(_item);
                break;
        }
    }

    

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.UpdateArmor);
    }
}
