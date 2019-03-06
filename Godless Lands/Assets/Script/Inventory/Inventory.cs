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
        if (inventory.items == null) return null;
        foreach (ItemCell itemCell in inventory.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetItem();
        }

        return inventory.armor.GetItem(key);
    }
    public static int GetCount(int key)
    {
        if (inventory.items == null) return 0;
       foreach(ItemCell itemCell in inventory.items)
        {
            if (itemCell.GetKey() == key) return itemCell.GetCount();
        }

        return inventory.armor.GetCount(key);
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
      //  size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;


        int index = nw.ReadInt();//Индекс изменившейся ячейки
        int key = nw.ReadInt();//Уnикальный ид предмета

        if (index >= 0 && index < items.Length)
        {
            items[index].SetKey(key);
            if (key == 0) { items[index].PutItem(null, 0); return; }
        }
        else return;
       

            int id_item = nw.ReadInt();//Индекс нового предмета в этой ячейке
            int count = nw.ReadInt();
         items[index].PutItem(itemsList.CreateItem(id_item), count);

        if(nw.AvailableBytes > 0)
        {
            items[index].SetEnchantLevel(nw.ReadInt());
            items[index].SetDurabilty(nw.ReadInt());
            items[index].SetMaxDurabilty(nw.ReadInt());
        }


        if (refreshCount != null) refreshCount();
    }

    private void LoadingInventory(NetworkWriter nw)
    {
        int filling = nw.ReadInt();
        size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;
        items = new ItemCell[size_bag];

      //  int count_item;

        Item item;
        for (int i = 0; i < items.Length; i++)//Создать пустые ячейки
        {
            GameObject _obj = Instantiate(itemCell);
            _obj.transform.SetParent(bag);
            items[i] = _obj.GetComponent<ItemCell>();
            items[i].SetIndex(i);
        }

        while(nw.AvailableBytes > 0)
        {
            int index = nw.ReadInt();
            int key = nw.ReadInt();
            int id_item = nw.ReadInt();
            int count = nw.ReadInt();

            if (index >= 0 && index < items.Length)
            {
                
                items[index].SetKey(key);
                items[index].PutItem(itemsList.CreateItem(id_item), count);
            }

            if (nw.ReadBool())
            {
                int enchant_level = nw.ReadInt();
                int durability = nw.ReadInt();
                int maxDurability = nw.ReadInt();
                if (index > 0 && index < items.Length)
                {
                    items[index].SetEnchantLevel(enchant_level);
                    items[index].SetDurabilty(durability);
                    items[index].SetMaxDurabilty(maxDurability);
                }
            }
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
