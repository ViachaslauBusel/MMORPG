using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RUCP;
using System;
using Items;
using RUCP.Packets;
using RUCP.Network;

public class ArmorListener : MonoBehaviour {

    private InventoryArmor inventoryArmor;
    private Armor armor;
    private bool combatState = false;

    private void Awake()
    {
        inventoryArmor = GameObject.Find("Inventory").GetComponentInChildren<InventoryArmor>();
        armor = GetComponent<Armor>();
        armor.Init();
        HandlersStorage.RegisterHandler(Types.CombatState, CombatState);
        HandlersStorage.RegisterHandler(Types.UpdateArmor, UpdateArmor);
      //  RegisteredTypes.RegisterTypes(Types.LoadArmor, LoadArmor);
    }
    
    private void CombatState(Packet nw)
    {
        armor.SetCombatstate(nw.ReadBool());
    }

    private void LoadArmor(Packet nw)
    {
       while(nw.AvailableBytes >= 8)
        {
            UpdateArmor(nw);
        }
    }

    private void UpdateArmor(Packet nw)
    {
        ItemType type = (ItemType)nw.ReadInt();
        ArmorPart part = (ArmorPart)nw.ReadInt();
        Item item = nw.ReadItem();

       

        inventoryArmor.PutItem(type, item);//Отоброзить иконку в инвентаре
        armor.PutItem(type, item);
    }

    private Item ReadItem(Packet nw, int itemID)
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
        Packet nw = new Packet(Channel.Discard);
        nw.WriteType(Types.CombatState);
        nw.WriteBool(combatState);
        NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        HandlersStorage.UnregisterHandler(Types.UpdateArmor);
        HandlersStorage.UnregisterHandler(Types.CombatState);
      //  RegisteredTypes.UnregisterTypes(Types.LoadArmor);
    }
}
