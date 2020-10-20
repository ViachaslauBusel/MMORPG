using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Cells;
using Items;
using RUCP.Packets;

public delegate void Update();
public class Inventory : MonoBehaviour {

    public static Inventory Instance;

    public ItemsList itemsList;
   // public Transform bag;
    public GameObject itemCell;
    //public Text text_filling;
    private Canvas canvasInventory;
    public Bag bag;
    public Bag backpack;
   // private int size_bag;
    private InventoryArmor armor;

    private UISort uISort;
    private event Update update;



    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        armor = GetComponentInChildren<InventoryArmor>();

        HandlersStorage.RegisterHandler(Types.UpdateInventory, UpdateInventory);
        HandlersStorage.RegisterHandler(Types.UpdateItem, UpdateItem);
    }

    private void UpdateItem(Packet nw)
    {
        Item item = GetItemByObjectID(nw.ReadInt());
        if (item != null) item.durability = nw.ReadInt();
    }

    internal void Refresh()//Вызывается из ячеек экипировки при их обновлении
    {
     ///   update();
    }

    //Создает предмет по ид
    public static Item CreateItem(int id)
    {
       return Instance.itemsList.CreateItem(id);
    }
    public static Item GetItemByObjectID(int objectID)
    {
        if (Instance.bag.cells == null || Instance.backpack.cells == null) return null;
        foreach (ItemCell itemCell in Instance.bag.cells)
        {
            if (itemCell.GetObjectID() == objectID) return itemCell.GetItem();
        }
        foreach (ItemCell itemCell in Instance.backpack.cells)
        {
            if (itemCell.GetObjectID() == objectID) return itemCell.GetItem();
        }

        return Instance.armor.GetItem(objectID);
    }
    public static ItemCell GetItemCellByObjectID(int objectID)
    {
        if (Instance.bag.cells == null || Instance.backpack.cells == null) return null;
        foreach (ItemCell itemCell in Instance.bag.cells)
        {
            if (itemCell.GetObjectID() == objectID) return itemCell;
        }
        foreach (ItemCell itemCell in Instance.backpack.cells)
        {
            if (itemCell.GetObjectID() == objectID) return itemCell;
        }

        return Instance.armor.GetItemCell(objectID);
    }
    public static int GetCount(int key)
    {
        if (Instance.bag.cells == null || Instance.backpack.cells == null) return 0;
       foreach(ItemCell itemCell in Instance.bag.cells)
        {
            if (itemCell.GetObjectID() == key) return itemCell.GetCount();
        }
        foreach (ItemCell itemCell in Instance.backpack.cells)
        {
            if (itemCell.GetObjectID() == key) return itemCell.GetCount();
        }

        return Instance.armor.GetCount(key);
    }
    //Получить общее количество всех предметов в инвенторе по ид
    public static int GetAllCount(int id)
    {
        int allCount = 0;
        foreach (ItemCell itemCell in Instance.bag.cells)
        {
            if (itemCell.ID() == id) allCount += itemCell.GetCount();
        }
        foreach (ItemCell itemCell in Instance.backpack.cells)
        {
            if (itemCell.ID() == id) allCount += itemCell.GetCount();
        }
        return allCount;
    }
    public static void RegisterUpdate(Update refresh)
    {
        Instance.update += refresh;
    }
    public static void UnregisterUpdate(Update refresh)
    {
        Instance.update -= refresh;
    }

    public ItemCell[] GetCellItems()
    {
        ItemCell[] items = new ItemCell[bag.cells.Count + backpack.cells.Count];
        bag.cells.CopyTo(items, 0);
        backpack.cells.CopyTo(items, bag.cells.Count);
        return items;
    }

    private void Start()
    {
        uISort = GetComponentInParent<UISort>();
        canvasInventory = GetComponent<Canvas>();
        canvasInventory.enabled = false;
    }

    private void UpdateInventory(Packet nw)//Обновление содержимого ячейки
    {

        switch (nw.ReadByte())
        {
            case 3:
                bag.UpdateInventory(nw);
                break;
            case 1:
                backpack.UpdateInventory(nw);
                break;
        }

        update();
    }


    


    public void OpenCloseInventory()
    {
        
        canvasInventory.enabled = !canvasInventory.enabled;

        if(canvasInventory.enabled) uISort.PickUp(canvasInventory);
    }


    private void OnDestroy()
    {
     //   RegisteredTypes.UnregisterTypes(Types.LoadInventory);
        HandlersStorage.UnregisterHandler(Types.UpdateInventory);
        HandlersStorage.UnregisterHandler(Types.UpdateItem);
    }


}

