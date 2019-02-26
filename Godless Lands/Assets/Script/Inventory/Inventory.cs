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
    public Transform bag;
    public GameObject itemCell;
    public Text text_filling;
    private Canvas canvasInventory;
    private ItemCell[] items;
    private int size_bag;

    private UISort uISort;
    private RefreshCount refreshCount;

    private static Inventory inventory;

    private void Awake()
    {
        if (inventory != null) Destroy(gameObject);
        inventory = this;
        RegisteredTypes.RegisterTypes(Types.LoadInventory, LoadingInventory);
        RegisteredTypes.RegisterTypes(Types.UpdateInventory, UpdateInventory);
    }
    //Создает предмет по ид
    public static Item GetItem(int id)
    {
       return inventory.itemsList.GetItem(id);
    }
    public static int GetCount(int key)
    {
        if (inventory.items == null) return 0;
       foreach(ItemCell itemCell in inventory.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetCount();
        }
        return 0;
    }
    //Получить общее количество всех предметов в инвенторе по ид
    public static int GetAllCount(int id)
    {
        int allCount = 0;
        foreach (ItemCell itemCell in inventory.items)
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
        int filling = nw.ReadInt();//Заполнение рюкзака
        size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;
        int index = nw.ReadInt();//Индекс изменившейся ячейки
        int id_item = nw.ReadInt();//Индекс нового предмета в этой ячейке
        int key = nw.ReadInt();//Уnикальный ид предмета

        int count = 0;
        Item item;
        if (id_item > 0)//Если предмет существует 
        {
            count = nw.ReadInt();
           
        }
      //  print("id: " + id_item);
        item = itemsList.GetItem(id_item);
        //  print("item: " + (item == null));
        items[index].Refresh(item, count, key);

        if (refreshCount != null) refreshCount();
    }

    private void LoadingInventory(NetworkWriter nw)
    {
        int filling = nw.ReadInt();
        size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;
        items = new ItemCell[size_bag];

        int count_item;

        Item item;
        for (int i=0; i<items.Length; i++)
        {
            int id_item = nw.ReadInt();
            item = itemsList.GetItem(id_item);
            int key = nw.ReadInt();
            count_item = 0;
            if (item != null)//Если ячейка не пуста
            {
                count_item = nw.ReadInt();
            }

            GameObject _obj = Instantiate(itemCell);
            _obj.transform.SetParent(bag);
            ItemCell _itemCell = _obj.GetComponent<ItemCell>();
            _itemCell.PutItem(item, count_item);
            _itemCell.SetIndex(i);
            _itemCell.SetKey(key);
            items[i] = _itemCell;
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
    }
}
