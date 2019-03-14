using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Cells;
using Items;

public delegate void RefreshCount();
public class Inventory : MonoBehaviour {

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
    private RefreshCount refreshCount;

    private static Inventory inventory;

    private void Awake()
    {
        if (inventory != null) Destroy(gameObject);
        inventory = this;
        armor = GetComponentInChildren<InventoryArmor>();
        RegisteredTypes.RegisterTypes(Types.LoadInventory, LoadingInventory);
        RegisteredTypes.RegisterTypes(Types.UpdateInventory, UpdateInventory);
        RegisteredTypes.RegisterTypes(Types.UpdateItem, UpdateItem);
    }

    private void UpdateItem(NetworkWriter nw)
    {
        Item item = GetItem(nw.ReadInt());
        if (item != null) item.durability = nw.ReadInt();
    }

    internal void Refresh()//Вызывается из ячеек экипировки при их обновлении
    {
        refreshCount();
    }

    //Создает предмет по ид
    public static Item CreateItem(int id)
    {
       return inventory.itemsList.CreateItem(id);
    }
    public static Item GetItem(int key)
    {
        if (inventory.bag.items == null || inventory.backpack.items == null) return null;
        foreach (ItemCell itemCell in inventory.bag.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetItem();
        }
        foreach (ItemCell itemCell in inventory.backpack.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetItem();
        }

        return inventory.armor.GetItem(key);
    }
    public static int GetCount(int key)
    {
        if (inventory.bag.items == null || inventory.backpack.items == null) return 0;
       foreach(ItemCell itemCell in inventory.bag.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetCount();
        }
        foreach (ItemCell itemCell in inventory.backpack.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetCount();
        }

        return inventory.armor.GetCount(key);
    }
    //Получить общее количество всех предметов в инвенторе по ид
    public static int GetAllCount(int id)
    {
        int allCount = 0;
        foreach (ItemCell itemCell in inventory.bag.items)
        {
            if (itemCell.ID() == id) allCount += itemCell.GetCount();
        }
        foreach (ItemCell itemCell in inventory.backpack.items)
        {
            if (itemCell.ID() == id) allCount += itemCell.GetCount();
        }
        return allCount;
    }
    public static void RegisterCount(RefreshCount refresh)
    {
        if(inventory.refreshCount == null)
            inventory.refreshCount = refresh;
        else
        inventory.refreshCount += refresh;
    }
    public static void UnregisterCount(RefreshCount refresh)
    {
        if (inventory.refreshCount == null) return;

            inventory.refreshCount -= refresh;
    }

    public ItemCell[] GetCellItems()
    {
        ItemCell[] items = new ItemCell[bag.items.Length + backpack.items.Length];
        bag.items.CopyTo(items, 0);
        backpack.items.CopyTo(items, bag.items.Length);
        return items;
    }

    private void Start()
    {
        uISort = GetComponentInParent<UISort>();
        canvasInventory = GetComponent<Canvas>();
        canvasInventory.enabled = false;
    }

    private void UpdateInventory(NetworkWriter nw)//Обновление содержимого ячейки
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
        if (refreshCount != null) refreshCount();
    }

    private void LoadingInventory(NetworkWriter nw)
    {
        switch (nw.ReadByte())
        {
            case 3:
                bag.LoadingInventory(nw, itemsList);
                break;
            case 1:
                backpack.LoadingInventory(nw, itemsList);
                break;
        }

        if (refreshCount != null) refreshCount();
    }

    


    public void OpenCloseInventory()
    {
        
        canvasInventory.enabled = !canvasInventory.enabled;

        if(canvasInventory.enabled) uISort.PickUp(canvasInventory);
    }


    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.LoadInventory);
        RegisteredTypes.UnregisterTypes(Types.UpdateInventory);
        RegisteredTypes.UnregisterTypes(Types.UpdateItem);
    }
}
