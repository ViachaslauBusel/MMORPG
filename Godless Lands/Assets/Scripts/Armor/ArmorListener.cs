﻿using Items;
using RUCP;
using RUCP.Handler;
using UnityEngine;
using Zenject;

public class ArmorListener : MonoBehaviour {

    private InventoryArmor inventoryArmor;
    private Armor armor;
    private bool combatState = false;
    private NetworkManager networkManager;

    [Inject]
    private void Constructor(NetworkManager networkManager)
    {
        this.networkManager = networkManager;
        //networkManager.RegisterHandler(Types.CombatState, CombatState);
        //networkManager.RegisterHandler(Types.UpdateArmor, UpdateArmor);
    }
    private void Awake()
    {
        inventoryArmor = GameObject.Find("Inventory").GetComponentInChildren<InventoryArmor>();
        armor = GetComponent<Armor>();
       
      //  RegisteredTypes.RegisterTypes(Types.LoadArmor, LoadArmor);
    }
    
    private void CombatState(Packet nw)
    {
        armor.SetCombatstate(nw.ReadBool());
    }

    private void LoadArmor(Packet nw)
    {
       while(nw.AvailableBytesForReading >= 8)
        {
            UpdateArmor(nw);
        }
    }

    private void UpdateArmor(Packet nw)
    {
        ItemType type = (ItemType)nw.ReadInt();
        ArmorPart part = (ArmorPart)nw.ReadInt();
        Item item = null;// nw.ReadItem();

       

        inventoryArmor.PutItem(type, item);//Отоброзить иконку в инвентаре
        armor.PutItem(type, item);
    }

    private Item ReadItem(Packet nw, int itemID)
    {
        Item _item = null;// InventoryWindow.CreateItem(itemID);
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
    //TODO msg
        //Packet nw = new Packet(Channel.Discard);
        //nw.WriteType(Types.CombatState);
        //nw.WriteBool(combatState);
        //NetworkManager.Send(nw);
    }

    private void OnDestroy()
    {
        //networkManager?.UnregisterHandler(Types.UpdateArmor);
        //networkManager?.UnregisterHandler(Types.CombatState);
      //  RegisteredTypes.UnregisterTypes(Types.LoadArmor);
    }
}
