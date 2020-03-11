using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using Items;

public class ArmorListener : MonoBehaviour {

    private InventoryArmor inventoryArmor;
    private Armor armor;
    private bool combatState = false;

    private void Awake()
    {
        inventoryArmor = GameObject.Find("Inventory").GetComponentInChildren<InventoryArmor>();
        armor = GetComponent<Armor>();
        armor.Init();
        RegisteredTypes.RegisterTypes(Types.CombatState, CombatState);
        RegisteredTypes.RegisterTypes(Types.UpdateArmor, UpdateArmor);
      //  RegisteredTypes.RegisterTypes(Types.LoadArmor, LoadArmor);
    }
    
    private void CombatState(NetworkWriter nw)
    {
        armor.SetCombatstate(nw.ReadBool());
    }

    private void LoadArmor(NetworkWriter nw)
    {
       while(nw.AvailableBytes >= 8)
        {
            UpdateArmor(nw);
        }
    }

    private void UpdateArmor(NetworkWriter nw)
    {
        ItemType type = (ItemType)nw.ReadInt();
        ArmorPart part = (ArmorPart)nw.ReadInt();
        Item item = nw.ReadItem();

       

        inventoryArmor.PutItem(type, item);//Отоброзить иконку в инвентаре
        armor.PutItem(type, item);
    }

    private Item ReadItem(NetworkWriter nw, int itemID)
    {
        Item _item = Inventory.CreateItem(itemID);
        _item.objectID = nw.ReadInt();
        _item.count = nw.ReadInt();
        _item.enchant_level = nw.ReadInt();
        _item.durability = nw.ReadInt();
        _item.maxDurability = nw.ReadInt();

        return _item;
    }


    private void Update()
    {
        if (Input.GetButtonDown("R"))
        {
            combatState = !combatState;
            armor.SetCombatstate(combatState);
            SendState();
        }
    }

    private void SendState()
    {
        NetworkWriter nw = new NetworkWriter(Channels.Reliable | Channels.Discard);
        nw.SetTypePack(Types.CombatState);
        nw.write(combatState);
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.UpdateArmor);
        RegisteredTypes.UnregisterTypes(Types.CombatState);
      //  RegisteredTypes.UnregisterTypes(Types.LoadArmor);
    }
}
