using RUCP;
using RUCP.Handler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Cells;
using Items;

public class Inventory : MonoBehaviour {

    public ItemsList itemsList;
    public Transform bag;
    public GameObject itemCell;
    public Text text_filling;
    private Canvas inventory;
    private ItemCell[] items;
    private int size_bag;

    private UISort uISort;

    private void Awake()
    {
        RegisteredTypes.RegisterTypes(Types.LoadInventory, LoadingInventory);
        RegisteredTypes.RegisterTypes(Types.UpdateInventory, UpdateInventory);
    }

    public ItemCell[] GetCellItems()
    {
        return items;
    }

    private void Start()
    {
        uISort = GetComponentInParent<UISort>();
        inventory = GetComponent<Canvas>();
        inventory.enabled = false;
    }

    private void UpdateInventory(NetworkWriter nw)//Обновление содержимого ячейки
    {
        int filling = nw.ReadInt();//Заполнение рюкзака
        size_bag = nw.ReadInt();
        text_filling.text = filling + "/" + size_bag;
        int index = nw.ReadInt();//Индекс изменившейся ячейки
        int id_item = nw.ReadInt();//Индекс нового предмета в этой ячейке
        int count = 0;
        Item item;
        if (id_item > 0)//Если предмет существует 
        {
            count = nw.ReadInt();
           
        }
      //  print("id: " + id_item);
        item = itemsList.GetItem(id_item);
      //  print("item: " + (item == null));
        items[index].SetCount(count);
        items[index].PutItem(item);
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
            count_item = 0;
            if (item != null)//Если ячейка не пуста
            {
                count_item = nw.ReadInt();
            }

            GameObject _obj = Instantiate(itemCell);
            _obj.transform.SetParent(bag);
            ItemCell _itemCell = _obj.GetComponent<ItemCell>();
            _itemCell.PutItem(item);
            _itemCell.SetCount(count_item);
            _itemCell.SetIndex(i);
            items[i] = _itemCell;
        }
    }

    


    public void OpenCloseInventory()
    {
        
        inventory.enabled = !inventory.enabled;

        if(inventory.enabled) uISort.PickUp(inventory);
    }


    private void OnDestroy()
    {
        RegisteredTypes.UnregisterTypes(Types.LoadInventory);
        RegisteredTypes.UnregisterTypes(Types.UpdateInventory);
    }
}
